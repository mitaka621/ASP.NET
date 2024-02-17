using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    [Comment("Type of event")]
	public class Type
	{
        [Key]
        [Comment("Unique identifier for the type of event")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Types.MaxNameLength)]
        [Comment("Type name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Collection of the events of the current type")]
        public IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}
