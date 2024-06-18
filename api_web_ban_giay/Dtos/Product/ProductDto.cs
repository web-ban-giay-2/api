
using api_web_ban_giay.Dtos.TradeMark;
using api_web_ban_giay.Models;
using System.Drawing;

namespace api_web_ban_giay.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Trademark { get; set; } = string.Empty ;
        public bool TrangThai { get; set; }
        public List<Models.Image>? Images { get; set; }
        public List<ProductDetail>? ProductDetails { get; set; }
    }
}
