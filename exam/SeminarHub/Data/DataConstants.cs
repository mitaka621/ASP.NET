namespace SeminarHub.Data
{
    public static class DataConstants
    {
        public static class Seminar
        {
            public const int MinTopicLength = 3;
            public const int MaxTopicLength = 100;

            public const int MinLecturerLength = 5;
            public const int MaxLecturerLength = 60;

            public const int MinDetailsLength = 10;
            public const int MaxDetailsLength = 500;

            public const int MinDuration = 30;
            public const int MaxDuration = 180;

            public const string DateTimeFormat = "dd/MM/yyyy HH:mm";
        }

        public static class Category
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public static class ErrorMessages
        {
            public const string LengthError= "The length should be between {2} and {1}";

            public const string Required = "This field is required!";

            public const string OutOfRange = "Value should be in the range: {1} to {2}";

            public const string InvalidDateTime="The date and time are invalid. They should be in the format: "+Seminar.DateTimeFormat;
        }
    }
}
