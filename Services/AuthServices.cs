using KnowCloud.Models;
using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;

namespace KnowCloud.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IBaseService _baseService;

        public AuthServices(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.POST,
                Data= registrationRequestDto,
                Url= Utility.Utilities.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto() { 
                ApiType = Utility.Utilities.ApiType.POST,
                Data= loginRequestDto,
                Url=Utility.Utilities.AuthAPIBase+"/api/AuthApi/login"
            
            });
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.POST,
                Data = registrationRequestDto,
                Url = Utility.Utilities.AuthAPIBase + "/api/auth/register"

            });
        }
    }
}
