using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
    [Comment("Class entity representing a seminar")]
    public class Seminar
    {
        [Key]
        [Comment("Seminar unique identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Seminar.MaxTopicLength)]
        [Comment("Seminar topic")]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(DataConstants.Seminar.MaxLecturerLength)]
        [Comment("Seminar Lecturer ")]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [MaxLength(DataConstants.Seminar.MaxDetailsLength)]
        [Comment("Seminar Details")]
        public string Details { get; set; } = string.Empty;

        [Required]
        [Comment("Foreign key for the organizer of the seminar.")]
        public string OrganizerId { get; set; }=string.Empty;
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("Event Date and time")]
        public DateTime DateAndTime { get; set; }

        [Range(DataConstants.Seminar.MinDuration, DataConstants.Seminar.MaxDuration)]
        [Comment("Seminar duration")]
        public int? Duration { get; set; }

        [Required]
        [Comment("Foreign key for the event's category")]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public IEnumerable<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}
