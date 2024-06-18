using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_web_ban_giay.Data;
using api_web_ban_giay.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore;

namespace api_web_ban_giay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly api_web_ban_giay_Context _context;
        private readonly IWebHostEnvironment _webhost;

        public ImageController(api_web_ban_giay_Context context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;
        }

        // GET: api/Image
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImage()
        {
            return await _context.Image.ToListAsync();
        }

        // GET: api/Image/5
        [HttpGet]
        [Route("get-pro-img/{fileName}")]
        public async Task<ActionResult<Image>> GetImageName(string fileName)
        {
            var imagePath = Path.Combine("wwwroot", "img", "product", fileName); // Đường dẫn tới hình ảnh trong thư mục wwwroot

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); // Trả về hình ảnh dưới dạng file stream
            }
            else
            {
                return NotFound(); // Không tìm thấy hình ảnh
            }
        }

        // PUT: api/Image/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> PutImage([FromForm] List<IFormFile> images, int ProductId)
        {
            var ImageOld = await _context.Image.Where(x => x.ProductId == ProductId).ToListAsync();
            if(ImageOld != null)
            {
                foreach (var image in ImageOld)
                {
                    _context.Image.Remove(image);
                    _context.SaveChanges();
                    string filePath = Path.Combine(_webhost.WebRootPath, "img/product", image.Name);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }

            foreach (var image in images)
            {
                if (image != null)
                {
                    string uploadDir = Path.Combine(_webhost.WebRootPath, "img/product"); // đưa ảnh vào file
                    string fileName = Guid.NewGuid().ToString().Substring(0, 5) + "_" + image.FileName;
                    string filePath = Path.Combine(uploadDir, fileName); // đưa ảnh vào file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    var img = new Image();
                    img.Name = fileName;
                    img.ProductId = ProductId;
                    _context.Image.Add(img);
                    _context.SaveChanges();
                }
            }
            return Ok(images);
        }


        // POST: api/Image
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{IdProduct}")]
        public async Task<ActionResult<Image>> PostImage([FromForm] List<IFormFile> images, int IdProduct)
        {
            foreach (var image in images)
            {
                if (image != null)
                {
                    string uploadDir = Path.Combine(_webhost.WebRootPath, "img/product"); // đưa ảnh vào file
                    string fileName = Guid.NewGuid().ToString().Substring(0, 5) + "_" + image.FileName;
                    string filePath = Path.Combine(uploadDir, fileName); // đưa ảnh vào file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    var img = new Image();
                    img.Name = fileName;
                    img.ProductId = IdProduct;
                    _context.Image.Add(img);
                    _context.SaveChanges();
                }
            }
            return Ok(images);
        }

        // DELETE: api/Image/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Image.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.Id == id);
        }
    }
}
