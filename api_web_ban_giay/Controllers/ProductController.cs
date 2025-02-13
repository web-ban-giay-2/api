﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_ban_giay.Data;
using api_web_ban_giay.Models;
using api_web_ban_giay.Mappers;
using api_web_ban_giay.Dtos.Product;

namespace api_web_ban_giay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly api_web_ban_giay_Context _context;

        public ProductController(api_web_ban_giay_Context context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var product = await _context.Product
                .Where(x => x.TrangThai == true)
                .Include(x => x.Images)
                .Include(x => x.Trademark)
                .Include(x => x.ProductDetails)
                .OrderByDescending(x => x.Id)
                .Take(8)
                .ToListAsync();
            return Ok(product.Select(x => x.ToGetProdductDto()));
        }

        [HttpGet("getAll-form-admin/{page}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllFormAdmin(int page)
        {
            int pageSize = 5;
            int totalItems = await _context.Product.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            if (page < 1 || page > totalPages)
            {
                return BadRequest("Invalid page number");
            }
            var product = await _context.Product
                .Include(x => x.Images)
                .Include(x => x.Trademark)
                .Include(x => x.ProductDetails)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            /*return Ok(product.Select(x => x.ToGetProdductDto()));*/
            return Ok(new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                Items = product.Select(x => x.ToGetProdductDto())
            });
        }
        [HttpGet("sp-tuongtu-form-home/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductTuongTu(int id)
        {
            var pro = await _context.Product.FindAsync(id);
            var product = await _context.Product
                .Where(x => x.TrangThai == true)
                .Include(x => x.Images)
                .Include(x => x.Trademark)
                .Include(x => x.ProductDetails)
                .Where(x => x.TrademarkId == pro.TrademarkId)
                .OrderByDescending(x => x.Id)
                .Take(6)
                .ToListAsync();
            return Ok(product.Select(x => x.ToGetProdductDto()));
        }
        [HttpGet("getAll-form-shop")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllFormShop()
        {
            var product = await _context.Product
                .Where(x => x.TrangThai == true)
                .Include(x => x.Images)
                .Include(x => x.Trademark)
                .Include(x => x.ProductDetails)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return Ok(product.Select(x => x.ToGetProdductDto()));
        }

        [HttpGet]
        [Route("home-banchay")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductBanChay()
        {
            var donhang_ct = await _context.Dh_ChiTiet
                .Include(x => x.DonHang)
                .Where(x => x.DonHang.TrangThai_DonHang == "Đã giao")
                .GroupBy(x => x.ProductId)
                .ToListAsync();

            var sumQuantitiesByProduct = donhang_ct.Select(g => new
            {
                ProductId = g.Key,
                TotalQuantity = g.Sum(x => x.SoLuong)
            }).OrderByDescending(x => x.TotalQuantity)
                .ToList();

            var Allproduct = await _context.Product
                .Where(x => x.TrangThai == true)
                .Include(x => x.Images)
                .Include(x => x.Trademark)
                .Include(x => x.ProductDetails)
                .ToListAsync();
            if(Allproduct != null)
            {
                var orderedProducts = Allproduct
                    .Join(sumQuantitiesByProduct,
                          p => p.Id,
                          q => q.ProductId,
                          (p, q) => new { Product = p, q.TotalQuantity })
                    .OrderByDescending(x => x.TotalQuantity)
                    .Select(x => x.Product)
                    .Take(6)
                    .ToList();
                return Ok(orderedProducts.Select(x => x.ToGetProdductDto()));
            } 
            else
            {
                return NoContent();
            }
            
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product
                .Include (x => x.Images)
                .Include(x => x.Trademark)
                .Include(x => x.ProductDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("get-product-form-home/{id}")]
        public async Task<ActionResult<Product>> GetProductFromHome(int id)
        {
            var product = await _context.Product
                .Where(x => x.TrangThai == true)
                .Include(x => x.Images)
                .Include(x => x.Trademark)
                .Include(x => x.ProductDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToGetProdductDto());
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new {id = product.Id}, product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] CreateProductRequestDto productDto)
        {
            var productModel = productDto.ToCreateProductRequestDto();

            _context.Product.Add(productModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = productModel.Id }, productModel);
        }


        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.TrangThai = false;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                code = 200,
                message = "Xoa thanh cong"
            });
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
