using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_ban_giay.Data;
using api_web_ban_giay.Models;
using api_web_ban_giay.Dtos.TaiKhoan;
using System.Runtime.InteropServices;

namespace api_web_ban_giay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly api_web_ban_giay_Context _context;

        public TaiKhoanController(api_web_ban_giay_Context context)
        {
            _context = context;
        }

        // GET: api/TaiKhoan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetTaiKhoan()
        {
            return await _context.TaiKhoan.ToListAsync();
        }



        // GET: api/TaiKhoan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiKhoan>> GetTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoan.FindAsync(id);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return taiKhoan;
        }

        [HttpGet("getAll-user")]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetAllUser()
        {
            var taiKhoan = await _context.TaiKhoan
                .Where(x => x.IsAdmin == false)
                .ToListAsync();

            return taiKhoan;
        }

        [HttpGet]
        [Route("dang-nhap-user/{taiKhoan}/{matKhau}")]
        public async Task<IActionResult> DangnhapUser(string taiKhoan, string matKhau)
        {
            var taiKhoan1 = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.Username == taiKhoan && x.Password == matKhau);
            if(taiKhoan1 == null)
            {
                return NotFound();
            } else
            {
                if (taiKhoan1.IsAdmin == false)
                   
                {
                    return Ok(taiKhoan1);
                    
                }
                return NotFound();
            }
        }

        [HttpGet]
        [Route("dang-nhap-admin/{taiKhoan}/{matKhau}")]
        public async Task<IActionResult> DangnhapAdmin(string taiKhoan, string matKhau)
        {
            var taiKhoan1 = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.Username == taiKhoan && x.Password == matKhau);
            if (taiKhoan1 == null)
            {
                return NotFound();
            }
            else
            {
                if (taiKhoan1.IsAdmin == true)

                {
                    return Ok(taiKhoan1);
                }
                return NotFound();

            }
        }
        [HttpPost("ban-tk/{id}")]
        public async Task<IActionResult> BanTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoan
                .FirstOrDefaultAsync(x => x.Id == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            taiKhoan.TrangThai = false;
            _context.TaiKhoan.Update(taiKhoan);
            await _context.SaveChangesAsync();
            return Ok(taiKhoan);
        }
        [HttpPost("mo-ban-tk/{id}")]
        public async Task<IActionResult> MoBanTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoan
                .FirstOrDefaultAsync(x => x.Id == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            taiKhoan.TrangThai = true;
            _context.TaiKhoan.Update(taiKhoan);
            await _context.SaveChangesAsync();
            return Ok(taiKhoan);
        }

        // PUT: api/TaiKhoan/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiKhoan(int id, TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.Id)
            {
                return BadRequest();
            }

            _context.Entry(taiKhoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiKhoanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaiKhoan
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("dang-ky-user")]
        public async Task<ActionResult<TaiKhoan>> PostTaiKhoan(TaiKhoan taiKhoan)
        {
            var tk = _context.TaiKhoan.FirstOrDefault(x => x.Username == taiKhoan.Username);
            if(tk != null)
            {
                return Ok(new
                {
                    code = 400,
                    message = "Tài khoản đã tồn tại"
                });
            }
            _context.TaiKhoan.Add(taiKhoan);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                code = 200,
                message = "Đăng ký thành công"
            });
        }

        // DELETE: api/TaiKhoan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            _context.TaiKhoan.Remove(taiKhoan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoan.Any(e => e.Id == id);
        }
    }
}
