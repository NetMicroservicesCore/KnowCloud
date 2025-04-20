using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;

namespace KnowCloud.Services
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;
        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public Task<ResponseDto> ApplyCouponAsync(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> EmailCart(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> GetCartByUserIdAsnyc(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.GET,
                Url=Utility.Utilities.ShoppingCartAPIBase+"/api/coupon/GetCart"+userId
            });
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
