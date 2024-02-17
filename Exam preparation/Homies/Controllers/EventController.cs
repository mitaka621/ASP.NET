using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Security.Claims;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext dbContext;
        public EventController(HomiesDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await dbContext.Events.Select(x => new EventInfoViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                StartTime = x.Start.ToString(DataConstants.Event.DateTimeFormat),
                Type = x.Type.Name,
                Owner = x.Organiser.UserName
            }).AsNoTracking()
            .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new EventFormViewModel()
            {
                Types = await GetTypes()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            DateTime start = DateTime.Now;

            DateTime end = DateTime.Now;

            DateTime currentTime = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.Start,
                DataConstants.Event.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start), DataConstants.ErrorMessages.InvalidDateTime);
            }

            if (!DateTime.TryParseExact(
                model.End,
                DataConstants.Event.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End), DataConstants.ErrorMessages.InvalidDateTime);
            }

            if (!ModelState.IsValid)
            {
                return View(new EventFormViewModel()
                {
                    Types = await GetTypes()
                });
            }

            var newEvent = new Event()
            {
                OrganiserId = GetUserId(),
                Name = model.Name,
                Description = model.Description,
                Start = start,
                End = end,
                CreatedOn = currentTime,
                TypeId = model.TypeId,
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var events = await dbContext.EventsParticipants.Where(x => x.HelperId == GetUserId()).Select(y => new EventInfoViewModel()
            {
                Id = y.Event.Id,
                Name = y.Event.Name,
                StartTime = y.Event.Start.ToString(DataConstants.Event.DateTimeFormat),
                Type = y.Event.Type.Name,
                Owner = y.Event.Organiser.UserName
            }).AsNoTracking()
               .ToListAsync();

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            if (await dbContext.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) == null
                || await dbContext.EventsParticipants.AsNoTracking().AnyAsync(x => x.HelperId == GetUserId() && x.EventId == id))
            {
                return RedirectToAction("All");
            }

            var newParticipation = new EventParticipant()
            {
                HelperId = GetUserId(),
                EventId = id
            };

            dbContext.EventsParticipants.Add(newParticipation);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await dbContext.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) == null
                || !await dbContext.EventsParticipants.AsNoTracking().AnyAsync(x => x.HelperId == GetUserId() && x.EventId == id))
            {
                return RedirectToAction("All");
            }

            var newParticipation = new EventParticipant()
            {
                HelperId = GetUserId(),
                EventId = id
            };

            dbContext.EventsParticipants.Remove(newParticipation);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ev = await dbContext.Events.AsNoTracking().Where(x => x.Id == id).Select(x=> new EventInfoViewModel()
            {
                Name = x.Name,
                StartTime = x.Start.ToString(DataConstants.Event.DateTimeFormat),
                EndTime = x.End.ToString(DataConstants.Event.DateTimeFormat),
                CreatedOn = x.CreatedOn.ToString(DataConstants.Event.DateTimeFormat),
                Type = x.Type.Name,
                Description = x.Description,
                Owner = x.Organiser.UserName
            }).FirstOrDefaultAsync();
            if (ev == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var types = await GetTypes();
            var ev = await dbContext.Events.AsNoTracking().Where(x => x.Id == id).Select(x => new EventFormViewModel()
            {
                Name = x.Name,
                Start = x.Start.ToString(DataConstants.Event.DateTimeFormat),
                End = x.End.ToString(DataConstants.Event.DateTimeFormat),                
                TypeId = x.Type.Id,
                Types= types,
                Description = x.Description,
            }).FirstOrDefaultAsync();

            if (ev == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(ev);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model,int id)
        {
            DateTime start = DateTime.Now;

            DateTime end = DateTime.Now;

            DateTime currentTime = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.Start,
                DataConstants.Event.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start), DataConstants.ErrorMessages.InvalidDateTime);
            }

            if (!DateTime.TryParseExact(
                model.End,
                DataConstants.Event.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End), DataConstants.ErrorMessages.InvalidDateTime);
            }

            if (!ModelState.IsValid)
            {
                model.Types =await GetTypes();
                return View(model);
            }

            var ev = await dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

            ev.OrganiserId = GetUserId();
            ev.Name = model.Name;
            ev.Description = model.Description;
            ev.Start = start;
            ev.End = end;
            ev.CreatedOn = currentTime;
            ev.TypeId = model.TypeId;
            

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

            private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<List<TypeViewModel>> GetTypes()
        {
            return await dbContext.Types.Select(x => new TypeViewModel()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToListAsync();
        }
    }
}
