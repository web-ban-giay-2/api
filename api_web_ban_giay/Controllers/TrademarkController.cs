using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_ban_giay.Data;
using api_web_ban_giay.Models;
using api_web_ban_giay.Dtos.TradeMark;
using api_web_ban_giay.Dtos.Product;
using api_web_ban_giay.Mappers;

namespace api_web_ban_giay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrademarkController : ControllerBase
    {
        private readonly api_web_ban_giay_Context _context;

        public TrademarkController(api_web_ban_giay_Context context)
        {
            _context = context;
        }

        // GET: api/Trademark
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trademark>>> GetTrademark()
        {
            return await _context.Trademark.ToListAsync();
        }

        // GET: api/Trademark/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trademark>> GetTrademark(int id)
        {
            var trademark = await _context.Trademark.FindAsync(id);

            if (trademark == null)
            {
                return NotFound();
            }

            return Ok(trademark);
        }

        // PUT: api/Trademark/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrademark(int id, [FromBody] UpdateDto trademarkDto)
        {
            if (id != trademarkDto.Id)
            {
                return BadRequest();
            }
            var trademark = trademarkDto.ToUpdateDto();

            _context.Entry(trademark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrademarkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetTrademark), new {id = trademark.Id }, trademark);
        }

        // POST: api/Trademark
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trademark>> PostTrademark([FromBody] CreateTradeMarkDto trademarkDto)
        {
            var productModel = trademarkDto.ToCreateTradeMarRequestkDto();
            _context.Trademark.Add(productModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrademark", new { id = productModel.Id }, productModel);
        }

        // DELETE: api/Trademark/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrademark(int id)
        {
            var trademark = await _context.Trademark.FindAsync(id);
            if (trademark == null)
            {
                return NotFound();
            }

            _context.Trademark.Remove(trademark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrademarkExists(int id)
        {
            return _context.Trademark.Any(e => e.Id == id);
        }
    }
}
