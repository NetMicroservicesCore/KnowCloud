using KnowCloud.Utility;
using static KnowCloud.Utility.Utilities;

namespace KnowCloud.Models.Dto
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

        //esta propiedad nos sirve para utilizarse a trabajar con documentos o imagenes y transportar sus datos.
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
