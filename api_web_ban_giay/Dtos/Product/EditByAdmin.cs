using api_web_ban_giay.Models;

namespace api_web_ban_giay.Dtos.Product
{
    public class EditByAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool TrangThai { get; set; } = true;
        public int TrademarkId { get; set; }
        public Trademark? Trademark { get; set; }
        public List<Image>? Images { get; set; }
        public List<ProductDetail>? ProductDetails { get; set; }
    }



}
