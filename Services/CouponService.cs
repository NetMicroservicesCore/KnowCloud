using KnowCloud.Contract;
using KnowCloud.Models.Dto;

namespace KnowCloud.Services
{
    public class CouponService : ICouponService
    {
        public Task<ResponseDto> CreateCouponsAsync(CouponDto couponDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> DeleteCouponsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetAllCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetCouponAsync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetCouponByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> UpdateCouponsAsync(CouponDto couponDto)
        {
            throw new NotImplementedException();
        }
    }
}
