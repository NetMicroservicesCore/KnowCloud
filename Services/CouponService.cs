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
                Url = Utility.Utilities.CouponAPIBase + "/api/Cupon"
            });
        }

        public async Task<ResponseDto> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.DELETE,
                Url = Utility.Utilities.CouponAPIBase + "/api/Cupon/" + id
            });
        }

        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.GET,
                Url = Utility.Utilities.CouponAPIBase + "/api/Cupon"
            });
        }

        /// <summary>
        /// Metodo que se encarga de consultar un cupon por el codigo del cupon
        /// </summary>
        /// <param name="couponCode">el codigo del cupon</param>
        /// <returns>una tarea que regresa el responseDto como entidad de respuesta</returns>
        public async Task<ResponseDto> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.GET,
                Url = Utility.Utilities.CouponAPIBase + "/api/Cupon/GetByCode/"+couponCode
            });
        }

        /// <summary>
        /// este metodo se encarga de obtener un cupon por el identificador del cupon
        /// </summary>
        /// <param name="id">identificador del cupon</param>
        /// <returns>una tarea con la entidad ResponseDto de por medio</returns>
        public async Task<ResponseDto> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.GET,
                Url = Utility.Utilities.CouponAPIBase + "/api/Cupon/" + id
            });
        }

        /// <summary>
        /// Este metodo se encarga de la actualziacion de los cupones a traves del serviceCoupon
        /// </summary>
        /// <param name="couponDto">una entidad del tipo CouponDto</param>
        /// <returns>una tarea con la respuesta envuelta en un dto</returns>
        public async Task<ResponseDto> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.PUT,
                Data = couponDto,
                Url = Utility.Utilities.CouponAPIBase + "/api/Cupon"
            });
        }
    }
}
