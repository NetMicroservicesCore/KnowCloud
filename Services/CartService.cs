using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;

namespace KnowCloud.Services
{
    public class CartService : ICartService
    {
        public Task<ResponseDto> ApplyCouponAsync(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> EmailCart(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetCartByUserIdAsnyc(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> RemoveFromCartAsync(int cartDetailsId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> UpsertCartAsync(CartDto cartDto)
        {
            throw new NotImplementedException();
        }
    }
}
