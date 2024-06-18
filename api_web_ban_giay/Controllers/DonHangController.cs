using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_ban_giay.Data;
using api_web_ban_giay.Models;
using api_web_ban_giay.Dtos.Cart;
using api_web_ban_giay.Mappers;
using api_web_ban_giay.DTOs;
using api_web_ban_giay.General;

namespace api_web_ban_giay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly api_web_ban_giay_Context _context;
        private readonly IConfiguration _configuration;

        public DonHangController(api_web_ban_giay_Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/DonHang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHang()
        {
            var pro = await _context.Product.ToListAsync();
            var dh = await _context.DonHang
                .Include(x => x.TaiKhoan)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Trademark)
                .ToListAsync();
            return Ok(dh.Select(x => x.ToGetAllDonHangDto()));
        }
        [HttpGet]
        [Route("ChoDuyet")]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangChoDuyet()
        {
            var dh = await _context.DonHang
                .Where(x => x.TrangThai_DonHang == "Chờ duyệt")
                .OrderByDescending(x => x.TimeCreate)
                .Include(x => x.TaiKhoan)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Trademark)
                .ToListAsync();
            return Ok(dh.Select(x => x.ToGetAllDonHangDto()));
        }

        [HttpGet]
        [Route("DangGiao")]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangDangGiao()
        {
            var dh = await _context.DonHang
                .Where(x => x.TrangThai_DonHang == "Đang giao")
                .OrderByDescending(x => x.TimeCreate)
                .Include(x => x.TaiKhoan)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Trademark)
                .ToListAsync();
            return Ok(dh.Select(x => x.ToGetAllDonHangDto()));
        }

        [HttpGet]
        [Route("DaGiao")]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangDaGiao()
        {
            var dh = await _context.DonHang
                .Where(x => x.TrangThai_DonHang == "Đã giao")
                .OrderByDescending(x => x.TimeCreate)
                .Include(x => x.TaiKhoan)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Trademark)
                .ToListAsync();
            return Ok(dh.Select(x => x.ToGetAllDonHangDto()));
        }

        [HttpGet]
        [Route("DaHuy")]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangDaHuy()
        {
            var dh = await _context.DonHang
                .Where(x => x.TrangThai_DonHang == "Đã huỷ")
                .OrderByDescending(x => x.TimeCreate)
                .Include(x => x.TaiKhoan)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .Include(x => x.Dh_ChiTiets)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Trademark)
                .ToListAsync();
            return Ok(dh.Select(x => x.ToGetAllDonHangDto()));
        }

        [HttpGet]
        [Route("ThongKe")]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangThongKe()
        {
            var end = DateTime.Now;
            var start = end.AddMonths(-12);

            var donHang_thongKe = await _context.DonHang
                .Where(x => x.TimeCreate >= start && x.TimeCreate <= end)
                .Include(x => x.Dh_ChiTiets)
                .GroupBy(x => new { x.TimeCreate.Year, x.TimeCreate.Month })
                .OrderBy(group => group.Key.Year)
                .ThenBy(group => group.Key.Month)
                .Select(group => new
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    DaGiao = group.Count(x => x.TrangThai_DonHang == "Đã giao"),
                    DaHuy = group.Count(x => x.TrangThai_DonHang == "Đã huỷ"),
                })
                .ToListAsync();

            return Ok(donHang_thongKe);
        }
        [HttpPost]
        [Route("OrderPayVNPay")]

        public async Task<ActionResult> OrderPayVNPay(VnpayDTOs model)
        {
            var vnp_Url = _configuration["Vnpay:Url"];

            var vnp_Returnurl = _configuration["Vnpay:ReturnUrl"];

            var vnp_TmnCode = _configuration["Vnpay:TmnCode"];

            var vnp_HashSecret = _configuration["Vnpay:HashSecret"];

            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.1.0");

            vnpay.AddRequestData("vnp_Command", "pay");

            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);

            vnpay.AddRequestData("vnp_Amount", $"{(model.Amount * 100)}000");

            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

            vnpay.AddRequestData("vnp_CurrCode", "VND");

            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");

            vnpay.AddRequestData("vnp_Locale", "vn");

            vnpay.AddRequestData("vnp_OrderInfo", model.Id);

            vnpay.AddRequestData("vnp_OrderType", "other");

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);

            vnpay.AddRequestData("vnp_TxnRef", model.Id);

            return Ok(new SingleResponse
            {
                code = 200,
                message = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret)
            });
        }

        [HttpGet("vnpay-return")]
        public async Task<IActionResult> VnPayReturn()
        {
            var vnp_HashSecret = _configuration["Vnpay:HashSecret"];

            var vnpayData = HttpContext.Request.Query;

            VnPayLibrary vnpay = new VnPayLibrary();

            foreach (var (key, value) in vnpayData)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp"))
                {
                    vnpay.AddResponseData(key, value);
                }
            }

            string inputHash = vnpayData["vnp_SecureHash"];

            if (vnpay.ValidateSignature(inputHash, vnp_HashSecret))
            {
                string id = vnpayData["vnp_TxnRef"];

                string? code = vnpayData["vnp_ResponseCode"];

                if (code == "00")
                {
                    var dh = await _context.DonHang.FirstOrDefaultAsync(x => x.Id.ToString() == id);
                    dh.TrangThai_ThanhToan = true;
                    _context.DonHang.Update(dh);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Redirect($"https://app-user-a-fish.vercel.app/user-order");
                    }

                }
                else
                {
                    return Redirect("/payment-error");
                }
            }
            return BadRequest("Invalid signature");
        }


        // POST: api/DonHang
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DonHang>> PostDonHang(List<CartRequestDto> cart)
        {
            var UserId = cart[0].UserId;
            var donhang = new DonHang();
            donhang.TaiKhoanId = UserId;
            _context.DonHang.Add(donhang);
            
            if(await _context.SaveChangesAsync() > 0)
            {
                foreach(var item in cart)
                {
                    var chitiet = new Dh_ChiTiet();
                    chitiet.DonHangId = donhang.Id;
                    chitiet.ProductId = item.ProductId;
                    chitiet.SoLuong = item.Quantity;
                    chitiet.Size = item.Size;
                    _context.Dh_ChiTiet.Add(chitiet);
                }
                await _context.SaveChangesAsync();
            }

            return Ok("Done");
        }

        // DELETE: api/DonHang/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonHang(int id)
        {
            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut]
        [Route("DuyetDon/{id}")]
        public async Task<IActionResult> DuyetDon(int id)
        {
            var donHang = await _context.DonHang
                .FirstOrDefaultAsync(donHang => donHang.Id == id);
            if(donHang == null)
            {
                return NoContent();
            } else
            {
                if(donHang.TrangThai_DonHang == "Chờ duyệt")
                {
                    donHang.TrangThai_DonHang = "Đang giao";
                    
                }
                else
                {   donHang.TrangThai_ThanhToan = true;
                    donHang.TrangThai_DonHang = "Đã giao";
                    
                }
                await _context.SaveChangesAsync();
                return Ok("Done");
            }
            
        }
        [HttpPut]
        [Route("HuyDon/{id}")]
        public async Task<IActionResult> HuyDon(int id)
        {
            var donHang = await _context.DonHang
                .FirstOrDefaultAsync(donHang => donHang.Id == id);
            if (donHang == null)
            {
                return NoContent();
            }
            else
            {
                if (donHang.TrangThai_DonHang == "Chờ duyệt")
                {
                    donHang.TrangThai_DonHang = "Đã huỷ";

                }
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetDonHang", null);
            }

        }

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.Id == id);
        }
    }
}
