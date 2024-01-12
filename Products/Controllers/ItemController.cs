using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;
using System;
using System.Security.Cryptography;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
            private readonly MyContext _Context;
            public ItemController(MyContext context)
            {
                _Context = context;
            }

        [HttpGet]
        public async Task<IActionResult> AllItems()
        {
         List<Item> items = await _Context.Items.ToListAsync();
            return Ok(items); 
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] mdlItem mdl)
        {
            using var stream = new MemoryStream();
            await mdl.Image.CopyToAsync(stream);
            var item = new Item
            {
                Name = mdl.Name,
                Price = mdl.Price,
                CategoryId = mdl.CategoryId,
                Image = stream.ToArray()
            };
            await _Context.Items.AddAsync(item);
            await _Context.SaveChangesAsync();
            return Ok(item);
        }


        [HttpGet("{id:int}", Name = "GetItem")]
        public async Task<IActionResult> GetItem(int id)
        {
            Item item = await _Context.Items.FirstOrDefaultAsync(d => d.Id == id);

            if (item == null)
            {
                return NotFound("Category id Not Found"); // HTTP 404 Not Found
            }

            return Ok(item);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateItem(mdlItem item)
        {
            Item it = await _Context.Items.FirstOrDefaultAsync(d => d.Id == item.id);

            if (it == null)
            {
                return NotFound($"Item with id {item.id} not found"); // HTTP 404 Not Found
            }

            using var stream = new MemoryStream();

         
            if (item.Image != null)
            {
                await item.Image.CopyToAsync(stream);
                it.Image = stream.ToArray();
            }

            it.Name = item.Name;
            it.Price = item.Price;

            await _Context.SaveChangesAsync();

            return Ok(it);
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveItem(int id)
        {
          Item item = await _Context.Items.FirstOrDefaultAsync(d => d.Id == id);

            _Context.Items.Remove(item);
            _Context.SaveChangesAsync();

            if (item== null)
            {
                return NotFound("Category id Not Found"); // HTTP 404 Not Found
            }

            return StatusCode(201, item);
        }

    }
}
