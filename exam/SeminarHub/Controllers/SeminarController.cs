using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;
using Seminar = SeminarHub.Data.Models.Seminar;


namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new SeminarFormViewModel() { Categories = await GetCategories() });
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            DateTime dateAndTime = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.DateAndTime,
                DataConstants.Seminar.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateAndTime))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), DataConstants.ErrorMessages.InvalidDateTime);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            context.Seminars.Add(new Seminar()
            {
                OrganizerId = GetUserId(),
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = dateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var seminars = await context.Seminars
                  .AsNoTracking()
                  .Select(x => new SeminarInfoViewModel()
                  {
                      Id = x.Id,
                      Organizer = x.Organizer.UserName,
                      Topic = x.Topic,
                      Lecturer = x.Lecturer,
                      Category = x.Category.Name,
                      DateAndTime = x.DateAndTime.ToString(DataConstants.Seminar.DateTimeFormat)
                  }).ToListAsync();


            return View(seminars);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var seminars = await context.SeminarsParticipants
                .AsNoTracking()
                .Where(x => x.ParticipantId == GetUserId())
                .Select(x => new SeminarInfoViewModel()
                {
                    Id = x.Seminar.Id,
                    Organizer = x.Seminar.Organizer.UserName,
                    Topic = x.Seminar.Topic,
                    Lecturer = x.Seminar.Lecturer,
                    Category = x.Seminar.Category.Name,
                    DateAndTime = x.Seminar.DateAndTime.ToString(DataConstants.Seminar.DateTimeFormat)
                }).ToListAsync();


            return View(seminars);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            bool isAlreadyJoined = await context.SeminarsParticipants
                   .AsNoTracking()
                   .AnyAsync(x => x.ParticipantId == GetUserId() && id == x.SeminarId);

            if (isAlreadyJoined)
            {
                return RedirectToAction(nameof(All));
            }

            var currentSeminar = await context.Seminars
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (currentSeminar == null)
            {
                return RedirectToAction(nameof(All));
            }

            context.SeminarsParticipants.Add(new SeminarParticipant()
            {
                SeminarId = id,
                ParticipantId = GetUserId(),
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currentSeminar = await context.Seminars
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (currentSeminar == null || currentSeminar.OrganizerId != GetUserId())
            {
                return RedirectToAction(nameof(All));
            }

            return View(new SeminarFormViewModel()
            {
                Topic = currentSeminar.Topic,
                Lecturer = currentSeminar.Lecturer,
                Details = currentSeminar.Details,
                DateAndTime = currentSeminar.DateAndTime.ToString(DataConstants.Seminar.DateTimeFormat),
                Duration = currentSeminar.Duration,
                Categories = await GetCategories(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
        {
            DateTime dateAndTime = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.DateAndTime,
                DataConstants.Seminar.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateAndTime))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), DataConstants.ErrorMessages.InvalidDateTime);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            var currentSeminar = await context.Seminars.FirstOrDefaultAsync(x => x.Id == id);
            if (currentSeminar == null||currentSeminar.OrganizerId!=GetUserId())
            {
                return RedirectToAction(nameof(All));
            }

            currentSeminar.Topic = model.Topic;
            currentSeminar.Lecturer = model.Lecturer;
            currentSeminar.Details = model.Details;
            currentSeminar.DateAndTime = dateAndTime;
            currentSeminar.Duration = model.Duration;
            currentSeminar.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var record = await context.SeminarsParticipants
                 .FirstOrDefaultAsync(x => x.SeminarId == id && x.ParticipantId == GetUserId());

            if (record == null)
            {
                return RedirectToAction(nameof(All));
            }

            context.SeminarsParticipants.Remove(record);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var seminar = await context.Seminars
                  .Where(x => x.Id == id)
                  .Select(s => new SeminarInfoViewModel()
                  {
                      Id = s.Id,
                      Category = s.Category.Name,
                      DateAndTime = s.DateAndTime.ToString(DataConstants.Seminar.DateTimeFormat),
                      Details = s.Details,
                      Duration = s.Duration == null ? "Duration not specified!" : s.Duration.ToString(),
                      Lecturer = s.Lecturer,
                      Topic = s.Topic,
                      Organizer = s.Organizer.UserName,
                  }).ToListAsync();

            if (seminar[0] == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(seminar[0]);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await context.Seminars
                  .AsNoTracking()
                  .Where(x => x.Id == id)
                  .Select(s => new SeminarInfoViewModel()
                  {
                      Id = s.Id,
                      Category = s.Category.Name,
                      DateAndTime = s.DateAndTime.ToString(DataConstants.Seminar.DateTimeFormat),
                      Details = s.Details,
                      Duration = s.Duration == null ? "Duration not specified!" : s.Duration.ToString(),
                      Lecturer = s.Lecturer,
                      Topic = s.Topic,
                      Organizer = s.Organizer.UserName,
                  }).ToListAsync();

            if (seminar[0] == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(seminar[0]);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminar = await context.Seminars
                  .FirstOrDefaultAsync(x=>x.Id==id);

            var participents = await context.SeminarsParticipants
                .Where(x => x.SeminarId == id)
                .ToListAsync();

            if (seminar == null||seminar.OrganizerId!=GetUserId())
            {
                return RedirectToAction(nameof(All));
            }

            if (participents != null)
            {
                context.SeminarsParticipants.RemoveRange(participents);
            }

            context.Seminars.Remove(seminar);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<List<CategoryViewModel>> GetCategories()
        {
            return await context.Categories.Select(x => new CategoryViewModel
            {
                Name = x.Name,
                Id = x.Id,
            }).ToListAsync();
        }
    }
}
