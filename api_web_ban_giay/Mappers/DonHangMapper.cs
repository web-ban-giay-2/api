using api_web_ban_giay.Dtos.DonHang;
using api_web_ban_giay.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;

namespace api_web_ban_giay.Mappers
{
    public static class DonHangMapper
    {
        public static DonHangDto ToGetAllDonHangDto(this DonHang donhang)
        {
            return new DonHangDto
            {
                Id = donhang.Id,
                TimeCreate = donhang.TimeCreate.ToShortDateString(),
                TrangThai_ThanhToan = donhang.TrangThai_ThanhToan,
                TrangThai_DonHang = donhang.TrangThai_DonHang,
                TenTaiKhoan = donhang.TaiKhoan.Username,
                TongTien = donhang.Dh_ChiTiets?.Sum(x => x.SoLuong * x.Product.Price) ?? 0,
                Dh_ChiTiets = donhang.Dh_ChiTiets?.Select(x => x.Dh_ChiTietToDonHang()).ToList(),
            };

        }

        public static Dh_ChiTietDto Dh_ChiTietToDonHang(this Dh_ChiTiet dh_ct)
        {
            return new Dh_ChiTietDto
            {
                Id = dh_ct.Id,
                Size = dh_ct.Size,
                Quantity = dh_ct.SoLuong,
                Price = dh_ct.Product?.Price ?? 0,
                NameProduct = dh_ct.Product?.Name ?? "",
                Image = dh_ct.Product?.Images?[0].Name ?? "",
                Trademark = dh_ct.Product?.Trademark?.Name ?? "",
                Tong = dh_ct.SoLuong * dh_ct.Product?.Price ?? 0
            };
        }
    }
}
