using TimetableWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TimetableWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly TimetableDbContext _timetableDbContext;
        public TimetableController(TimetableDbContext timetableDbContext)
        {
            _timetableDbContext = timetableDbContext;

        }
        [HttpGet]
        public ActionResult<IEnumerable<TimeTable>> GetTimetable()
        {
            return _timetableDbContext.Timetables;
        }
        [HttpGet("{timetableId:int}")]
        public async Task<ActionResult<TimeTable>> GetById(int timetableId)
        {
            var timetable = await _timetableDbContext.Timetables.FindAsync(timetableId);
            return timetable;
        }
        [HttpPost("addTimetable")]
        public async Task<ActionResult> Create(TimeTable timetable)
        {
            await _timetableDbContext.Timetables.AddAsync(timetable);
            await _timetableDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(TimeTable timeTable)
        {
            _timetableDbContext.Timetables.Update(timeTable);
            await _timetableDbContext.SaveChangesAsync();
            return Ok();
        }
       //[HttpDelete("DeleteByAccountId/{accountId}")]//
       // public async Task<IActionResult> DeleteByAccountId(int accountId)
       // {
       //     var timetable = await _timetableDbContext.Timetables.FindAsync(accountId);

       //     if (timetable == null)
       //     {
       //         return NotFound(); // Trả về 404 Not Found nếu không tìm thấy dòng dữ liệu cần xóa.
        //    }

       //     _timetableDbContext.Timetables.Remove(timetable);
       //     await _timetableDbContext.SaveChangesAsync();

       //     return NoContent(); // Trả về 204 No Content khi xóa thành công.
    //    }

        [HttpDelete("DeleteByTimetableId/{timetableId}")]
        public async Task<ActionResult<TimeTable>> DeleteByTimetableId(int timetableId)
        {
            var timetable = await _timetableDbContext.Timetables.FindAsync(timetableId);
            _timetableDbContext.Timetables.Remove(timetable);
            await _timetableDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("searchbyAccountId/{id}")]
        public async Task<IEnumerable<TimeTable>> SearchTimetables(int id)
        {
            var matchingTimeTables = _timetableDbContext.Timetables
                .Where(timetable => timetable.TimetableIdAccount == id).ToList();

            return matchingTimeTables;
        }

    }
}
