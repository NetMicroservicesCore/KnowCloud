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
