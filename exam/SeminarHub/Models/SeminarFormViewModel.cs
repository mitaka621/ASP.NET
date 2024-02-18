using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static SeminarHub.Data.DataConstants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SeminarHub.Models
{
    public class SeminarFormViewModel
    {

        [Required(ErrorMessage=ErrorMessages.Required)]
        [StringLength(Seminar.MaxTopicLength,
           MinimumLength = Seminar.MinTopicLength,
            ErrorMessage =ErrorMessages.LengthError)]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage=ErrorMessages.Required)]
        [StringLength(Seminar.MaxLecturerLength,
           MinimumLength = Seminar.MinLecturerLength,
            ErrorMessage = ErrorMessages.LengthError)]
        public string Lecturer { get; set; } = string.Empty;

        [Required(ErrorMessage=ErrorMessages.Required)]
        [StringLength(Seminar.MaxDetailsLength,
           MinimumLength = Seminar.MinDetailsLength,
            ErrorMessage = ErrorMessages.LengthError)]
        public string Details { get; set; } = string.Empty;


        [Required(ErrorMessage=ErrorMessages.Required)]
        public string DateAndTime { get; set; }=string.Empty;

        [Range(Seminar.MinDuration,Seminar.MaxDuration,ErrorMessage =ErrorMessages.OutOfRange)]
        public int? Duration { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int CategoryId { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
