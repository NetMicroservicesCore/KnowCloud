using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;

namespace KnowCloud.Services
{
    public class ProductService :IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Este metodo se encarga de crear un producto a traves de la pagina consumiendo el endpoint
        /// </summary>
        /// <param name="productDto">la entidad base</param>
        /// <returns>una respuesta del tipo responseDto</returns>
        public async Task<ResponseDto> CreateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType =Utility.Utilities.ApiType.POST,
                Data = productDto,
                Url= Utility.Utilities.ProductAPIBase+"/api/product",
                ContentType = Utility.Utilities.ContentType.MultipartFormData
            });
        }

        
        public async Task<ResponseDto> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.DELETE,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.GET,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product"
            });
        }

        public async Task<ResponseDto> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.GET,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDto> UpdateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utilities.ApiType.PUT,
                Data = productDto,
                Url = Utility.Utilities.ProductAPIBase + "/api/Product",
                ContentType = Utility.Utilities.ContentType.MultipartFormData
            });
        }


    }
}
