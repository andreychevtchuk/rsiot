﻿using Internet_Shop.Models;
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
    public class Shopping_cartController : Controller
    {
        ApplicationContext db;
        private readonly ILogger<Shopping_cartController> _logger;
        public Shopping_cartController(ILogger<Shopping_cartController> logger)
        {
            db = new ApplicationContext();
            _logger = logger;
        }
        //КОРЗИНА
        // POST api/products добавление заказа
        [HttpPost("Add_Shopping_Cart")]
        public async Task<ActionResult<Shopping_cart>> Add_shopping_cart(string address)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            Shopping_cart cart=new Shopping_cart { Address = address };
            db.Shopping_carts.Add(cart);
            await db.SaveChangesAsync();
            return Ok(cart);
        }
        // DELETE api/products/5 удаление заказа по id
        [HttpDelete("Delete_Shopping_Cart/{id}")]
        public async Task<ActionResult<Shopping_cart>> Delete_shopping_cart(int id)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            Shopping_cart cart = db.Shopping_carts.FirstOrDefault(x => x.Id == id);
            if (cart == null)
            {
                return NotFound();
            }
            db.Shopping_carts.Remove(cart);
            await db.SaveChangesAsync();
            return Ok(cart);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class Product_dataController : Controller
    {
        ApplicationContext db;
        private readonly ILogger<Shopping_cartController> _logger;
        public Product_dataController(ILogger<Shopping_cartController> logger)
        {
            db = new ApplicationContext();
            _logger = logger;
        }
        // POST api/products добавление данных о товаре для корзины
        [HttpPost("Add_In_Shopping_Cart")]
        public async Task<ActionResult<Product_in_cart>> Add_in_shopping_cart(int id_cart, int id_product, int quantity)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            if (id_cart <=0 || id_product <=0 || quantity <= 0)
            {
                return BadRequest();
            }
            Product p = await db.Products.FirstOrDefaultAsync(x => x.Id == id_product);
            if (p.Quantity >= quantity)
            {
                Product_in_cart product = new Product_in_cart { Id_cart = id_cart, Id_product = id_product, Quantity = quantity };
                db.Products_in_carts.Add(product);
                await db.SaveChangesAsync();
                p.Quantity -= quantity;
                db.Products.Update(p);
                await db.SaveChangesAsync();
                return Ok(product);
            }
            else
            {
                return BadRequest();
            }
        }
        // DELETE api/products/5 удаление товара по id
        [HttpDelete("Delete_From_Shopping_Cart/{id}")]
        public async Task<ActionResult<Product_in_cart>> Delete_from_shopping_cart(int id)
        {
            _logger.LogInformation(Request.GetDisplayUrl());
            Product_in_cart product = db.Products_in_carts.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            db.Products_in_carts.Remove(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }
    }
}
