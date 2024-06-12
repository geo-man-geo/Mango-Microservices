﻿using Mango.Web.Models;
using Mango.Web.Models.Utilities;
using Mango.Web.Service.IService;

#pragma warning disable CS8603 // Possible null reference return.

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> AddProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "/api/productAPI/",
                Data = productDto
            });

        }

        public async Task<ResponseDto> DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/productAPI/" + productId
            });
        }

        public async Task<ResponseDto> GetAllProductAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/productAPI"
            });
        }

        public async Task<ResponseDto> GetProductAsync(string productCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/productAPI/GetByCode" + productCode
            });
        }

        public async Task<ResponseDto> GetProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/productAPI/" + productId
            });
        }

        public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ProductAPIBase + "/api/productAPI/",
                Data = productDto
            });
        }
    }
}
