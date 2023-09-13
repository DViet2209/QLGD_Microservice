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
        [HttpGet("DanhSachThoiKhoaBieu")]
        public ActionResult<IEnumerable<TimeTable>> GetTimetable()
        {
            var TimeTable = _timetableDbContext.Timetables.ToList();

            if (TimeTable.Count == 0)
            {
                return NotFound("Không tìm thấy thời khóa biểu nào.");
            }
            else
            {
                return TimeTable;
            }
        }

        [HttpGet("XemThoiKhoaBieuTheoKhoaHocId")]
        public ActionResult<IEnumerable<TimeTable>> XemThoiKhoaBieuTheoKhoaHocId([FromQuery] int id)
        {
            var timeTablesForCourse = _timetableDbContext.Timetables
                .Where(tt => tt.TimetableCourseId == id)
                .ToList();

            if (timeTablesForCourse.Count == 0)
            {
                return NotFound($"Không tìm thấy thời khóa biểu cho khóa học có ID {id}.");
            }
            else
            {
                return timeTablesForCourse;
            }
        }

        [HttpPost("ThemThoiKhoaBieu")]
        public async Task<ActionResult> ThemThoiKhoaBieu(TimeTable TimeTable)
        {
            try
            {
                await _timetableDbContext.Timetables.AddAsync(TimeTable);
                await _timetableDbContext.SaveChangesAsync();
                return Ok("Thêm thời khóa biểu học thành công");
            }
            catch (Exception)
            {
                return NotFound("Thêm thời khóa biểu thất bại");
            }
        }
        [HttpGet("XemThoiKhoaBieuTheoNgay")]
        public ActionResult<IEnumerable<TimeTable>> XemThoiKhoaBieuTheoNgay([FromQuery] DateTime date)
        {
            var timeTablesForDate = _timetableDbContext.Timetables
                .Where(tt => tt.TimetableBeginTime.Date == date.Date)
                .ToList();

            if (timeTablesForDate.Count == 0)
            {
                return NotFound($"Không tìm thấy thời khóa biểu cho ngày {date.ToShortDateString()}.");
            }
            else
            {
                return timeTablesForDate;
            }
        }
        [HttpDelete("XoaThoiKhoaBieu/{timetableId}")]
        public ActionResult XoaThoiKhoaBieu(int timetableId)
        {
            var timetableToDelete = _timetableDbContext.Timetables.Find(timetableId);

            if (timetableToDelete == null)
            {
                return NotFound($"Không tìm thấy thời khóa biểu có ID {timetableId}.");
            }

            _timetableDbContext.Timetables.Remove(timetableToDelete);
            _timetableDbContext.SaveChanges();

            return Ok($"Đã xóa thời khóa biểu có id là {timetableId}");
        }
        [HttpPut("CapNhatThoiKhoaBieu")]
        public async Task<ActionResult> CapNhatThoiKhoaBieu(TimeTable TimeTable)
        {
            try
            {
                _timetableDbContext.Update(TimeTable);
                await _timetableDbContext.SaveChangesAsync();
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return NotFound("Không tìm thấy Thời khóa biểu.");
            }
        }
    }
}

