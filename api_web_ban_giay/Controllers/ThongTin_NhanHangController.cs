using api_web_ban_giay.Data;
using api_web_ban_giay.Dtos.ThongTin_NhanHang;
using api_web_ban_giay.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_web_ban_giay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTin_NhanHangController : ControllerBase
    {

        private readonly api_web_ban_giay_Context _context;

        public ThongTin_NhanHangController(api_web_ban_giay_Context context)
        {
            _context = context;
        }
        // POST api/<ThongTin_NhanHangController>
        [HttpPost]
        public async Task<IActionResult> PostThongTin_NhanHang([FromBody] ThongTin_NhanHangDto model)
        {
            if(model != null)
            {
                var tt_nh = new ThongTin_NhanHang();
                tt_nh.HoTen = model.HoTen;
                tt_nh.DiaChi = model.DiaChi;
                tt_nh.SDT = model.SDT;
                tt_nh.GhiChu = model.GhiChu;
                tt_nh.DonHangId = model.DonHangId;
                _context.ThongTin_NhanHang.Add(tt_nh);
                await _context.SaveChangesAsync();
                return Ok("Done");
            } else
            {
                return NoContent();
            }
        }

    }
}
