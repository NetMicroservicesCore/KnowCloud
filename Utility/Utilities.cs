namespace KnowCloud.Utility
{
    public class Utilities
    {
        public static string AuthAPIBase { get; set; }

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
