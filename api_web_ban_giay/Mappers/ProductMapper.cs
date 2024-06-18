using api_web_ban_giay.Dtos.Product;
using api_web_ban_giay.Models;


namespace api_web_ban_giay.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToGetProdductDto(this Product productModels)
        {
            return new ProductDto
            {
                Id = productModels.Id,
                Name = productModels.Name,
                Price = productModels.Price,
                Description = productModels.Description,
                Trademark = productModels.Trademark.Name,
                TrangThai = productModels.TrangThai,
                Images = productModels.Images,
                ProductDetails = productModels.ProductDetails,
            };
        }
        public static Product ToCreateProductRequestDto(this CreateProductRequestDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                TrademarkId = productDto.TrademarkId
            };
        }
         
    }
}
