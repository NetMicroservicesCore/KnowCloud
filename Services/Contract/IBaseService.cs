using KnowCloud.Models.Dto;

namespace KnowCloud.Services.Contract
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
