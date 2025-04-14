using KnowCloud.Contract;
using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;

namespace KnowCloud.Services
{
    public class CouponService : ICouponService
    {

        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Metodo para crear cupones dentro de la aplicacion web
        /// </summary>
        /// <param name="couponDto">recibe un objeto del tipo cupon</param>
        /// <returns>retorna una tarea con una respuesta del tipo ResponseDto</returns>
        public async Task<ResponseDto> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.POST,
                Data = couponDto,
                Url = Utility.Utilities.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.DELETE,
                Url = Utility.Utilities.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.GET,
                Url = Utility.Utilities.CouponAPIBase + "/api/coupon"
            });
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
