using System.Net;

namespace KnowCloud.Models
{
    public class ResponseWrapper<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
