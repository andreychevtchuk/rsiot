using Internet_Shop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

namespace Internet_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        ApplicationContext _db;

        private readonly ILogger<ProductsController> _logger;
        
        public ProductsController(ApplicationContext db, ILogger<ProductsController> logger)
        {
            _db = db;
            _logger = logger;
        }
        // получение всего списка товаров
        [HttpGet("Get_All_Products")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            return await _db.Products.ToListAsync();
        }

        // GET api/products/5 получение товара по id
        [HttpGet("Get_Product/{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            Product product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return NotFound();
            return new ObjectResult(product);
        }

        // POST api/products добавление нового товара
        [HttpPost("Add_Product")]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            if (product == null)
            {
                return BadRequest();
            }

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }

        // PUT api/products/ изменение параметров товара по id
        [HttpPut("Edit_Product")]
        public async Task<ActionResult<Product>> Edit(Product product)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            if (product == null)
            {
                return BadRequest();
            }
            if (!_db.Products.Any(x => x.Id == product.Id))
            {
                return NotFound();
            }

            _db.Update(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }

        // DELETE api/products/5 удаление товара по id
        [HttpDelete("Delete_Product/{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            Product product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }

       
    }
}
