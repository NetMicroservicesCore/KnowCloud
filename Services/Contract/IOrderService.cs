using KnowCloud.Models.Dto;

namespace KnowCloud.Services.Contract
{
    public interface IOrderService
    {
        Task<ResponseDto> CreateOrder(CartDto cartDto);
        Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto);
    } 
}
