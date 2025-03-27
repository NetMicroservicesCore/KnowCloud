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

        public Task<ResponseWrapper<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
