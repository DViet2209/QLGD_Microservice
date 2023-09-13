using Microsoft.AspNetCore.Mvc;
using CourseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("DanhSachTheLoaiLopHoc")]
        public ActionResult<IEnumerable<Category>> DanhSachTheLoaiLopHoc()
        {
            var Category = _categoryDbContext.Categories.ToList();

            if (Category.Count == 0)
            {
                return NotFound("Không tìm thấy loại khóa học nào.");
            }
            else
            {
                return Category;
            }
        }

        [HttpGet("TimTheLoaiLopHocTheoTen")]
        public async Task<ActionResult<IEnumerable<Category>>> TimTheLoaiLopHocTheoTen([FromQuery] string name)
        {
            var categories = await _categoryDbContext.Categories
                .Where(c => c.CategoryName.Contains(name))
                .ToListAsync();

            if (categories.Count == 0)
            {
                return NotFound("Không tìm thấy thể loại lớp học nào.");
            }

            return categories;
        }


        [HttpPost("ThemTheLoaiLopHoc")]
        public async Task<ActionResult> ThemTheLoaiLopHoc(Category category)
        {
            try
            {
                await _categoryDbContext.Categories.AddAsync(category);
                await _categoryDbContext.SaveChangesAsync();
                return Ok("Thêm loại lớp học thành công");
            }
            catch (Exception)
            {
                return NotFound("Thêm loại lớp học thất bại");
            }
        }

        [HttpPut("CapNhatTheLoaiLopHoc")]
        public async Task<ActionResult> CapNhatTheLoaiLopHoc(Category category)
        {
            try
            {
                _categoryDbContext.Categories.Update(category);
                await _categoryDbContext.SaveChangesAsync();
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return NotFound("Không tìm thấy loại lớp học.");
            }
        }

        [HttpDelete("XoaTheLoaiLopHoc")]
        public async Task<ActionResult<Category>> Delete([FromQuery] int id)
        {
            var category = await _categoryDbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("Không tìm thấy loại lớp học.");
            }

            _categoryDbContext.Categories.Remove(category);
            await _categoryDbContext.SaveChangesAsync();
            return Ok("Xóa thành công.");
        }
    }
}
