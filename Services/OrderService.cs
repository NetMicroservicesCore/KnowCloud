using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;

namespace KnowCloud.Services
{
    public class OrderService : IOrderService
    {
        public Task<ResponseDto> CreateOrder(CartDto cartDto)
        {
            throw new NotImplementedException();
        }
    }
}
