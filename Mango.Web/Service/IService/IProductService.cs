using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto> GetProductAsync(string ProductCode);
        Task<ResponseDto> GetAllProductAsync();
        Task<ResponseDto> GetProductByIdAsync(int ProductId);
        Task<ResponseDto> AddProductAsync(ProductDto ProductDto);
        Task<ResponseDto> UpdateProductAsync(ProductDto ProductDto);
        Task<ResponseDto> DeleteProductAsync(int ProductId);
    }
}
