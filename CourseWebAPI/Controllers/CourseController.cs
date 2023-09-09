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

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourse()
        {
            return _courseDbContext.Courses;
        }
        [HttpGet("{courseId:int}")]
        public async Task<ActionResult<Course>> GetById(int courseId)
        {
            var course = await _courseDbContext.Courses.FindAsync(courseId);
            return course;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Course course)
        {
            await _courseDbContext.Courses.AddAsync(course);
            await _courseDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Course course)
        {
            _courseDbContext.Courses.Update(course);
            await _courseDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{courseId:int}")]
        public async Task<ActionResult<Course>> Delete(int courseId)
        {
            var course = await _courseDbContext.Courses.FindAsync(courseId);
            _courseDbContext.Courses.Remove(course);
            await _courseDbContext.SaveChangesAsync();
            return Ok();
        }
        //Tim danh sach lop hoc theo MSSV//
    }
}
