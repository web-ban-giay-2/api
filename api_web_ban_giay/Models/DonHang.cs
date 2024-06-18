namespace api_web_ban_giay.Models
{
    public class DonHang
    {
        public int Id { get; set; }
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        public bool TrangThai_ThanhToan { get; set; } = false;
        public string TrangThai_DonHang { get; set; } = "Chờ duyệt";
        public int TaiKhoanId { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }
        public ICollection<Dh_ChiTiet>? Dh_ChiTiets { get; set; }
    }
}
