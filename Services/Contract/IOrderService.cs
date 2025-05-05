using KnowCloud.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KnowCloud.Services.Contract
{
    public interface IOrderService
    {
        Task<ResponseDto> CreateOrder(CartDto cartDto);
        Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto);

        Task<ResponseDto> ValidateStripeSession(int orderHeaderId);
    } 
}
