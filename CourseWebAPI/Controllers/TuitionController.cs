using CourseWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace CourseWebAPI.Controllers
{
    public class TuitionController : Controller
    {
        private readonly CourseDbContext _TuitionDbContext;
        public TuitionController(CourseDbContext TuitionDbContext)
        {
            _TuitionDbContext = TuitionDbContext;
        }

        [HttpGet("XemHocPhi/{tuitionId}")]
        public ActionResult XemHocPhi(int tuitionId)
        {
            var tuition = _TuitionDbContext.Tuitions.Find(tuitionId);

            if (tuition == null)
            {
                return NotFound($"Không tìm thấy học phí có ID {tuitionId}.");
            }

            return Ok(tuition);
        }

        [HttpGet("XemHocPhiTheoAccountId/{accountId}")]
        public ActionResult XemHocPhiTheoAccountId(string accountId)
        {
            var tuitionsForAccount = _TuitionDbContext.Tuitions
                .Where(t => t.AccountUserId == accountId)
                .ToList();

            if (tuitionsForAccount.Count == 0)
            {
                return NotFound($"Không tìm thấy học phí cho tài khoản có ID {accountId}.");
            }

            return Ok(tuitionsForAccount);
        }


        [HttpPost("ThemHocPhi")]
        public ActionResult ThemHocPhi([FromBody] Tuition newTuition)
        {
            try
            {
                _TuitionDbContext.Tuitions.Add(newTuition);
                _TuitionDbContext.SaveChanges();

                return Ok("Thêm học phí thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Thêm học phí thất bại: " + ex.Message);
            }
        }

        [HttpGet("DanhSachHocPhi")]
        public ActionResult DanhSachHocPhi()
        {
            var allTuitions = _TuitionDbContext.Tuitions.ToList();

            if (allTuitions.Count == 0)
            {
                return NotFound("Không tìm thấy bất kỳ học phí nào.");
            }

            return Ok(allTuitions);
        }



        [HttpPost("ThemHocVienKhoaHoc/{accID}")]
        public async Task<ActionResult> ThemHocVienKhoaHoc(Tuition tuition, string accID)
        {
            tuition.AccountUserId = accID;
            await _TuitionDbContext.Tuitions.AddAsync(tuition);
            await _TuitionDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("ThanhToanKhoaHoc")]
        public async Task<ActionResult> ThanhToanKhoaHoc(Tuition tuition)
        {
            _TuitionDbContext.Tuitions.Update(tuition);
            await _TuitionDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("XoaTatCaHocPhiTheoTaiKhoan/{accountId}")]
        public ActionResult XoaTatCaHocPhiTheoTaiKhoan(string accountId)
        {
            var tuitionsToDelete = _TuitionDbContext.Tuitions.Where(t => t.AccountUserId == accountId).ToList();

            if (tuitionsToDelete.Count == 0)
            {
                return NotFound($"Không tìm thấy học phí cho tài khoản có ID {accountId}.");
            }

            foreach (var tuition in tuitionsToDelete)
            {
                _TuitionDbContext.Tuitions.Remove(tuition);
            }

            _TuitionDbContext.SaveChanges();

            return NoContent();
        }
        [HttpPut("CapNhatHocPhi")]
        public async Task<ActionResult> CapNhatHocPhi(Tuition Tuition)
        {
            try
            {
                _TuitionDbContext.Tuitions.Update(Tuition);
                await _TuitionDbContext.SaveChangesAsync();
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return NotFound("Không tìm thấy học phi.");
            }
        }

    }
}
