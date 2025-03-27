using KnowCloud.Models;
using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;

namespace KnowCloud.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly 
        public Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
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
