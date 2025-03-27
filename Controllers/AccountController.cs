using KnowCloud.Models.Dto;
using KnowCloud.Services;
using KnowCloud.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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



    }
}
