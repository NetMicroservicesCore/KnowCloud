using KnowCloud.Models;
using KnowCloud.Models.Dto;

namespace KnowCloud.Services.Contract
{
    public interface IAuthServices
    {
        Task<ResponseWrapper<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
