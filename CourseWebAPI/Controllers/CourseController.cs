using CourseWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CourseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseDbContext _courseDbContext;
        public CourseController(CourseDbContext courseDbContext)
        {
            _courseDbContext = courseDbContext;
        }


        [HttpGet("DanhSachLophoc")]
        public ActionResult<IEnumerable<Course>> DanhSachLophoc()
        {
            var courses = _courseDbContext.Courses.ToList();

            if (courses.Count == 0)
            {
                return NotFound("Không tìm thấy khóa học nào.");
            }
            else
            {
                return courses;
            }
        }


        [HttpGet("TimLopHocTheoID")]
        public async Task<ActionResult<Course>> TimLopHocTheoID([FromQuery] int id)
        {
            var course = await _courseDbContext.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound("Không tìm thấy Lớp học nào.");

            }
            else
            {
                return course;
            }

        }

        [HttpGet("TimLopHocTheoGvID")]
        public async Task<ActionResult<IEnumerable<Course>>> TimLopHocTheoGvID([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Vui lòng cung cấp mã số giáo viên.");
            }

            var courses = await _courseDbContext.Courses
                .Where(c => c.TeacherAccountId.Contains(id))
                .ToListAsync();

            if (!courses.Any())
            {
                return NotFound($"Không tìm thấy lớp học nào do giáo viên có mã số {id} phụ trách.");
            }

            return courses;
        }


        [HttpPost("ThemLopHoc")]
        public async Task<ActionResult> ThemKhoaHoc(Course course)
        {
            try
            {
                await _courseDbContext.Courses.AddAsync(course);
                await _courseDbContext.SaveChangesAsync();
                return Ok("Thêm lớp học thành công");
            }
            catch (Exception)
            {

                return NotFound("Thêm lớp học thất bại - không tồn tại mã loại lớp học - thiếu hay định dạng dữ liệu chưa đúng");
            }
        }

        [HttpPut("CapNhatLopHoc")]
        public async Task<ActionResult> CapNhatLopHoc(Course course)
        {
            try
            {
                _courseDbContext.Courses.Update(course);
                await _courseDbContext.SaveChangesAsync();
                return Ok("Cập nhật thành công");
            }
            catch (Exception)
            {

                return NotFound("Cập nhật không thành công");
            }
        }

        [HttpDelete("XoaLopHocTheoID")]
        public async Task<ActionResult<Course>> XoaLopHocTheoID([FromQuery] int id)
        {
            try
            {
                var course = await _courseDbContext.Courses.FindAsync(id);
                _courseDbContext.Courses.Remove(course);
                await _courseDbContext.SaveChangesAsync();
                return Ok("Xóa lớp học thành công");
            }
            catch (Exception)
            {
                return NotFound("Không tìm thấy lớp học");

            }
        }

        [HttpGet("TimLopHocTheoNgay")]
        public async Task<ActionResult<IEnumerable<Course>>> TimLopHocTheoNgay([FromQuery] DateTime date)
        {
            var courses = await _courseDbContext.Courses
                .Where(c => c.CourseOpeningDay.Date == date.Date)
                .ToListAsync();

            if (!courses.Any())
            {
                return NotFound($"Không tìm thấy lớp học nào vào ngày {date.ToShortDateString()}");
            }

            return courses;
        }
        [HttpGet("TimLopHocTheoKhoangGia")]
        public async Task<ActionResult<IEnumerable<Course>>> TimLopHocTheoKhoangGia([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var courses = await _courseDbContext.Courses
                .Where(c => c.CourseTuition >= minPrice && c.CourseTuition <= maxPrice)
                .ToListAsync();

            if (!courses.Any())
            {
                return NotFound($"Không tìm thấy lớp học nào trong khoảng giá từ {minPrice} đến {maxPrice}");
            }

            return courses;
        }

    }
}
