using Microsoft.AspNetCore.Mvc;
using CourseWebAPI.Models;

namespace CourseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CourseDbContext _categoryDbContext;
        public CategoryController(CourseDbContext categoryDbContext)
        {
            _categoryDbContext = categoryDbContext;
        }


        [HttpGet("categories")] //Xem tat ca khoa hoc
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return _categoryDbContext.Categories;
        }
        [HttpPost("addCategory")] //Them the loai khoa hoc
        public async Task<ActionResult> AddNewCategory(Category category)
        {
            await _categoryDbContext.Categories.AddAsync(category);
            await _categoryDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Category category)
        {
            _categoryDbContext.Categories.Update(category);
            await _categoryDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{categoryId:int}")]
        public async Task<ActionResult<Category>> Delete(int categoryId)
        {
            var category = await _categoryDbContext.Categories.FindAsync(categoryId);
            _categoryDbContext.Categories.Remove(category);
            await _categoryDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
