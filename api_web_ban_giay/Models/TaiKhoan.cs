namespace api_web_ban_giay.Models
{
    public class TaiKhoan
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public DateTime? TimeCreate { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; } = false;
        public bool TrangThai { get; set; } = true;
    }
}
