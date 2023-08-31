using CategoryWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CategoryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryDbContext _categoryDbContext;
        public CategoryController(CategoryDbContext categoryDbContext)
        {
            _categoryDbContext = categoryDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            return _categoryDbContext.Categorys;
        }
        [HttpGet("{categoryId:int}")]
        public async Task<ActionResult<Category>> GetById(int categoryId)
        {
            var category = await _categoryDbContext.Categorys.FindAsync(categoryId);
            return category;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            await _categoryDbContext.Categorys.AddAsync(category);
            await _categoryDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Category category)
        {
            _categoryDbContext.Categorys.Update(category);
            await _categoryDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{categoryId:int}")]
        public async Task<ActionResult<Category>> Delete(int categoryId)
        {
            var category = await _categoryDbContext.Categorys.FindAsync(categoryId);
            _categoryDbContext.Categorys.Remove(category);
            await _categoryDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
