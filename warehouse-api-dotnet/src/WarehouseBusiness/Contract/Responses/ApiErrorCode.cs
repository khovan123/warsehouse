namespace Contract.Responses
{
    public static class ApiErrorCode
    {
        public const string ValidationFailed = "VALIDATION_FAILED";
        public const string AuthenticationFailed = "AUTHENTICATION_FAILED";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string Forbidden = "FORBIDDEN";
        public const string NotFound = "NOT_FOUND";
        public const string Conflict = "CONFLICT";
        public const string BadRequest = "BAD_REQUEST";
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
        public const string UnHandledException = "UNHANDED_EXCEPTION";
        public const string TooManyRequests = "TOO_MANY_REQUESTS";
        public const string InvalidRefreshToken = "INVALID_REFRESH_TOKEN";
        public const string TokenExpired = "TOKEN_EXPIRED";
    }
}
