namespace KnowCloud.Utility
{
    public class Utilities
    {
        public static string AuthAPIBase { get; set; }
        public const string TokenCookie = "JWTToken";
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public enum ApiType {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
