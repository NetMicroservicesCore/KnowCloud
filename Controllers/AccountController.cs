using KnowCloud.Models.Dto;
using KnowCloud.Services;
using KnowCloud.Services.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KnowCloud.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthServices _authService;
        private readonly ITokenProvider _tokenProvider;
        public AccountController(IAuthServices authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }


        [HttpGet]
        public IActionResult Perfil()
        {
            return View();
        }


        [HttpGet]
        public  IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto login)
        {
            ResponseDto responseDto = await _authService.LoginAsync(login);
            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                //establecemos nuestro login de autenticacion que proviene del api de autenticacion
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Perfil", "Account");
            }
           else {
                ModelState.AddModelError("CustomError",responseDto.Message);
                return View(login);
            }
        }


        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            //esto no va ya que viene del api de autenticacion
            //debemos cambiar al service adecuado
            var assignRoleSuccessful = await _authService.AssignRoleAsync(model);
            if (!assignRoleSuccessful.IsSuccess)
            {
                //procesamos la informacion adecuada
                return BadRequest();
            }
            return Ok();
        }


        private async Task SignInUser(LoginResponseDto login)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(login.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
                jwt.Claims.FirstOrDefault(u=> u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            var principal = new ClaimsPrincipal();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,);

        }


    }
}
