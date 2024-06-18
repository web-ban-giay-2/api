using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_web_ban_giay.Models;

namespace api_web_ban_giay.Data
{
    public class api_web_ban_giay_Context : DbContext
    {
        public api_web_ban_giay_Context (DbContextOptions<api_web_ban_giay_Context> options)
            : base(options)
        {
        }

        public DbSet<api_web_ban_giay.Models.Product> Product { get; set; } = default!;
        public DbSet<api_web_ban_giay.Models.Trademark> Trademark { get; set; } = default!;
        public DbSet<api_web_ban_giay.Models.Image> Image { get; set; } = default!;
        public DbSet<api_web_ban_giay.Models.ProductDetail> ProductDetail { get; set; } = default!;
        public DbSet<api_web_ban_giay.Models.TaiKhoan> TaiKhoan { get; set; } = default!;
        public DbSet<api_web_ban_giay.Models.Dh_ChiTiet> Dh_ChiTiet { get; set; } = default!;
        public DbSet<api_web_ban_giay.Models.DonHang> DonHang { get; set; } = default!;
    }

}
