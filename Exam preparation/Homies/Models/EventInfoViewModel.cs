using NuGet.Protocol.Plugins;

namespace Homies.Models
{
	public class EventInfoViewModel
	{
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

		public string StartTime { get; set; }=string.Empty;

        public string EndTime { get; set; }=string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Description { get; set; }=string.Empty;

        public string Owner { get; set; } = string.Empty;

        
    }
}
