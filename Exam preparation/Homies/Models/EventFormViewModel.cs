using static Homies.Data.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Homies.Data;
using Microsoft.EntityFrameworkCore;

namespace Homies.Models
{
	public class EventFormViewModel
	{
        public int Id { get; set; }
        [Required(ErrorMessage =ErrorMessages.Required)]
		[StringLength(
			Event.MaxNameLength,
			MinimumLength = Event.MinNameLength,
			ErrorMessage = ErrorMessages.LengthError)]
        public string Name { get; set; }=string.Empty;

        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(
			Event.MaxDescriptionLength,
			MinimumLength =Event.MinDescriptionLength,
			ErrorMessage = ErrorMessages.LengthError)]	
		public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.Required)]
        public string Start { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.Required)]
        public string End { get; set; } = string.Empty;

        public int TypeId { get; set; }

        public List<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}
