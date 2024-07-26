namespace api_web_ban_giay.Models
{
    public class ThongTin_NhanHang
    {
        public int Id { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string? GhiChu { get; set; }
        public int DonHangId { get; set; }
    }
}
