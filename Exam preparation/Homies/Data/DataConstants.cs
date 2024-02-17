using System.ComponentModel.DataAnnotations;

namespace Homies.Data
{
	public static class DataConstants
	{
		public static class Event
		{
			public const int MinNameLength = 5;

			public const int MaxNameLength = 20;

			public const int MinDescriptionLength = 15;

			public const int MaxDescriptionLength = 150;

			public const string DateTimeFormat = "yyyy-MM-dd H:mm";

        }

		public static class Types
		{
			public const int MinNameLength = 5;

			public const int MaxNameLength = 15;
		}

		public static class ErrorMessages
		{
			public const string LengthError = "Length must be between {2} and {1}!";

			public const string Required = "{0} is required!";

			public const string InvalidDateTime="Invalid DateTime. It should be in the format: "+ Event.DateTimeFormat;
        }

	}
}
