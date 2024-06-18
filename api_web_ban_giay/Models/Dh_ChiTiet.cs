namespace api_web_ban_giay.Models
{
    public class Dh_ChiTiet
    {
        public int Id { get; set; }
        public int DonHangId { get; set; }
        public DonHang? DonHang { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int SoLuong { get; set; }
        public int Size { get; set; }
    }
}
