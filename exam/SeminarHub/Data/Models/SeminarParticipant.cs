using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
    [Comment("Participents of the seminars")]
    public class SeminarParticipant
    {
        [Comment("Unique identifier for the seminar")]
        public int SeminarId { get; set; }
        [ForeignKey(nameof(SeminarId))]
        public Seminar Seminar { get; set; } = null!;

        [Comment("Unique identifier for the participant")]
        public string ParticipantId { get; set; } = string.Empty;
        [ForeignKey(nameof(ParticipantId))]
        public IdentityUser IdentityUser { get; set; }= null!;
    }
}