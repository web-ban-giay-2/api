namespace api_web_ban_giay.Dtos.Cart
{
    public class CartRequestDto
    {
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public int UserId { get; set; }
    }
}
