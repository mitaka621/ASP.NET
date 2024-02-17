using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
	[Comment("Events")]
	public class Event
	{
		[Key]
		[Comment("Unique identifier for the Event")]
		public int Id { get; set; }

		[Required]
		[MaxLength(DataConstants.Event.MaxNameLength)]
		[Comment("Event name")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[MaxLength(DataConstants.Event.MaxDescriptionLength)]
		[Comment("Event Description")]
		public string Description { get; set; } = string.Empty;

		[Required]
		[Comment("Foreign key for the organiser")]
		public string OrganiserId { get; set; } = string.Empty;
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; }=null!;

        [Required]
		[Comment("Date and time for the creation of the event")]
		public DateTime CreatedOn { get; set; }

		[Required]
		[Comment("Date and time for the start of the event")]
		public DateTime Start { get; set; }

		[Required]
		[Comment("Date and time for the start of the event")]
		public DateTime End { get; set; }

		[Required]
		[Comment("Foreign key for the type of event")]
        public int TypeId { get; set; }
		[ForeignKey(nameof(TypeId))]
		public Type Type { get; set; } = null!;

		[Comment("Collection containing all of the participants for the event")]
		public IEnumerable<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }
}
