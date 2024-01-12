using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyContext _Context;
        public CategoryController(MyContext context)
        {
            _Context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           List<Category> cat = await _Context.Categories.ToListAsync();

            return Ok(cat);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(Category Category)
        {
            if (ModelState.IsValid)
            {
                _Context.Categories.Add(Category);
                await _Context.SaveChangesAsync();

                string url = Url.Link("GetCate", new { id = Category.id });

                return Created(url, Category);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id:int}", Name = "GetCate")]
        public async Task<IActionResult> GetItem(int id)
        {
            Category cat = await _Context.Categories.FirstOrDefaultAsync(d => d.id == id);

            if (cat == null)
            {
                return NotFound("Category id Not Found"); // HTTP 404 Not Found
            }

            return Ok(cat);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateItem(Category Cate)
        {
            Category cat = await _Context.Categories.FirstOrDefaultAsync(d => d.id == Cate.id);

            cat.name = Cate.name;
            _Context.SaveChangesAsync();

            if (cat == null)
            {
                return NotFound("Category id Not Found"); // HTTP 404 Not Found
            }

            return StatusCode(201,cat);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItem(int id)
        {
            Category cat = await _Context.Categories.FirstOrDefaultAsync(d => d.id == id);

            _Context.Categories.Remove(cat);
            _Context.SaveChangesAsync();

            if (cat == null)
            {
                return NotFound("Category id Not Found"); // HTTP 404 Not Found
            }

            return StatusCode(201, cat);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> AlterItem([FromBody] JsonPatchDocument<Category> cat,
           [FromRoute] int id)
        {
            Category cate = await _Context.Categories.FirstOrDefaultAsync(d => d.id == id);

            if (cate == null)
            {
                return NotFound("Category Not Found"); // HTTP 404 Not Found
            }

            cat.ApplyTo(cate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _Context.SaveChangesAsync();

            return Ok(cate);
        }


    }
}
