namespace ExceptionHandling.FakeAPI;

public static class Constants
{
    public static class Endpoints
    {
        public const string Success = "success";
        public const string Unauthorized = "unauthorized";
        public const string Forbidden = "forbidden";
        public const string NotFound = "not-found";
        public const string TooManyRequests = "too-many-requests";
        public const string Internal = "internal";
    }
    
    public static class Messages
    {
        public const string Unauthorized = "You are unauthorized";
        public const string Forbidden = "Access is denied";
        public const string NotFound = "Data wasn't found";
        public const string TooManyRequests = "Too many requests";
        public const string Internal = "See logs for details";
    }
}