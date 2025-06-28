using KnowCloud.Models;
using KnowCloud.Models.Dto;

namespace KnowCloud.Services.Contract
{
    public interface IAuthServices
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto 
            registrationRequestDto);

        Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto
            registrationRequestDto);
    }
}
