using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_ban_giay.Data;
using api_web_ban_giay.Models;
using api_web_ban_giay.Dtos.NewFolder;
using api_web_ban_giay.Mappers;

namespace api_web_ban_giay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly api_web_ban_giay_Context _context;

        public ProductDetailController(api_web_ban_giay_Context context)
        {
            _context = context;
        }

        // GET: api/ProductDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetail>>> GetProductDetail()
        {
            return await _context.ProductDetail.ToListAsync();
        }

        // GET: api/ProductDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetail>> GetProductDetail(int id)
        {
            var productDetail = await _context.ProductDetail.FindAsync(id);

            if (productDetail == null)
            {
                return NotFound();
            }

            return Ok(productDetail);
        }

        [HttpGet]
        [Route("productId/{productId}")]
        public async Task<ActionResult<ProductDetail>> GetProductDetailPro(int productId)
        {
            var productDetail = await _context.ProductDetail.Where(x => x.ProductId == productId).ToListAsync();

            if (productDetail == null)
            {
                return NotFound();
            }

            return Ok(productDetail.Select(x => x.ToGetProdductDetailDto()));
        }

        // PUT: api/ProductDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductDetail(int id, ProductDetail productDetail)
        {
            if (id != productDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(productDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDetailExists(id))
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

        // POST: api/ProductDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{ProductId}")]
        public async Task<ActionResult<ProductDetail>> PostProductDetail([FromBody] List<ProductDetailDto> productDetail, int ProductId)
        {
            foreach (var item in productDetail)
            {
                var proDetail = new ProductDetail();
                proDetail.Size = item.Size;
                proDetail.Quantity = item.Quantity;
                proDetail.ProductId = ProductId;
                _context.ProductDetail.Add(proDetail);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductDetail), productDetail);
        }

        // DELETE: api/ProductDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            var productDetail = await _context.ProductDetail.FindAsync(id);
            if (productDetail == null)
            {
                return NotFound();
            }

            _context.ProductDetail.Remove(productDetail);
            await _context.SaveChangesAsync();

            return Ok("Done");
        }

        private bool ProductDetailExists(int id)
        {
            return _context.ProductDetail.Any(e => e.Id == id);
        }
    }
}
