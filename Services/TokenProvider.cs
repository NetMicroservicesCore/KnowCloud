using KnowCloud.Services.Contract;
using KnowCloud.Utility;

namespace KnowCloud.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(Utilities.TokenCookie);
        }

        public string GetToken()
        {
            string token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Utilities.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(Utilities.TokenCookie, token);
        }


    }
}
