using api_web_ban_giay.Models;

namespace api_web_ban_giay.Dtos.DonHang
{
    public class DonHangDto
    {
        public int Id { get; set; }
        public string? TimeCreate { get; set; }
        public bool TrangThai_ThanhToan { get; set; } = false;
        public string TrangThai_DonHang { get; set; } = "Chờ duyệt";
        public string TenTaiKhoan { get; set; } = string.Empty;
        public List<Dh_ChiTietDto>? Dh_ChiTiets { get; set; }
        public int TongTien { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string GhiChu { get; set; } = string.Empty;
    }

    public class Dh_ChiTietDto
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string? NameProduct { get; set; }
        public string? Image { get; set; }
        public string? Trademark { get; set; }
        public int Tong { get; set; }
    }
    public class tt_nh
    {
        
    }
}
