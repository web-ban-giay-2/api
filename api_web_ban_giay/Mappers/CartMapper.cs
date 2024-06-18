using api_web_ban_giay.Dtos.Cart;

namespace api_web_ban_giay.Mappers
{
    public static class CartMapper
    {
        public static CartRequestDto ToCartRequestDto(this CartRequestDto cart)
        {
            return new CartRequestDto
            {
                ProductId = cart.ProductId,
                Price = cart.Price,
                Quantity = cart.Quantity,
                Size = cart.Size,
            };
        }
    }
}
