namespace api_web_ban_giay.Dtos.Product
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TrademarkId { get; set; }
    }
}
