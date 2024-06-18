using api_web_ban_giay.Dtos.NewFolder;
using api_web_ban_giay.Dtos.Product;
using api_web_ban_giay.Models;

namespace api_web_ban_giay.Mappers
{
    public static class ProductDetailMapper
    {
        public static ProductDetailDto ToGetProdductDetailDto(this ProductDetail productModels)
        {
            return new ProductDetailDto
            {
                Size = productModels.Size,
                Quantity = productModels.Quantity,
            };
        }
    }
}
