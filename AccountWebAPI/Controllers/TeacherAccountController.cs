using AccountWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AccountWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAccountController : ControllerBase
    {

        private readonly AccountDbContext _teacheraccountDbContext;
        public TeacherAccountController(AccountDbContext teacheraccountDbContext)
        {
            _teacheraccountDbContext = teacheraccountDbContext;
        }
        [HttpGet("Teacheraccounts")]
        public ActionResult<IEnumerable<TeacherAccount>> GetTeacherAccount()
        {
            return _teacheraccountDbContext.Teacheraccounts;
        }

        [HttpGet("{teacheraccountId:int}")]
        public async Task<ActionResult<TeacherAccount>> GetById(int teacheraccountId)
        {
            var teacheraccount = await _teacheraccountDbContext.Teacheraccounts.FindAsync(teacheraccountId);
            return teacheraccount;
        }
        [HttpPost]
        public async Task<ActionResult> Create(TeacherAccount teacherAccount)
        {
            var outputParam = new SqlParameter("@output", System.Data.SqlDbType.NVarChar, 100);
            outputParam.Direction = System.Data.ParameterDirection.Output;

            await _teacheraccountDbContext.Database
                .ExecuteSqlRawAsync("EXEC dbo.pr_TeacherAccountUserId @output OUTPUT", outputParam);

            var newTeacherAccountID = outputParam.Value as string;

            teacherAccount.TeacherAccountUserId = newTeacherAccountID;
            await _teacheraccountDbContext.Teacheraccounts.AddAsync(teacherAccount);
            await _teacheraccountDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(TeacherAccount teacherAccount)
        {
            _teacheraccountDbContext.Teacheraccounts.Update(teacherAccount);
            await _teacheraccountDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{teacheraccountId:int}")]
        public async Task<ActionResult<TeacherAccount>> Delete(int teacheraccountId)
        {
            var teacheraccount = await _teacheraccountDbContext.Teacheraccounts.FindAsync(teacheraccountId);
            _teacheraccountDbContext.Teacheraccounts.Remove(teacheraccount);
            await _teacheraccountDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<TeacherAccount>> SearchAccounts([FromQuery] string keyword)
        {
            var matchingTeacherAccounts = _teacheraccountDbContext.Teacheraccounts
                .Where(account =>
                    account.TeacherAccountUserId.Contains(keyword) ||
                    account.TeacherAccountUserName.Contains(keyword) ||
                    account.TeacherAccountSurName.Contains(keyword))
                .ToList();

            return matchingTeacherAccounts;
        }
        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<TeacherAccount>> GetByUsername(string username)
        {
            var teacheraccount = await _teacheraccountDbContext.Teacheraccounts
                .FirstOrDefaultAsync(a => a.TeacherAccountUserName == username);

            if (teacheraccount == null)
                return NotFound();

            return teacheraccount;
        }
        [HttpGet("by-surname/{surname}")]
        public async Task<ActionResult<TeacherAccount>> GetBySurname(string surname)
        {
            var teacheraccount = await _teacheraccountDbContext.Teacheraccounts
                .FirstOrDefaultAsync(a => a.TeacherAccountSurName == surname);

            if (teacheraccount == null)
                return NotFound();

            return teacheraccount;
        }
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<TeacherAccount>> GetByEmail(string email)
        {
            var teacheraccount = await _teacheraccountDbContext.Teacheraccounts
                .FirstOrDefaultAsync(a => a.TeacherAccountEmail == email);

            if (teacheraccount == null)
                return NotFound();

            return teacheraccount;
        }
        [HttpGet("by-birthdate-range")]
        public ActionResult<IEnumerable<TeacherAccount>> GetByBirthdateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var teacheraccountsInDateRange = _teacheraccountDbContext.Teacheraccounts
                .Where(teacheraccount => teacheraccount.TeacherAccountBirthDate >= fromDate && teacheraccount.TeacherAccountBirthDate <= toDate)
                .ToList();

            return teacheraccountsInDateRange;
        }
        [HttpGet("by-sex/{sex}")]
        public ActionResult<IEnumerable<TeacherAccount>> GetBySex(string sex)
        {
            var teacheraccountsBySex = _teacheraccountDbContext.Teacheraccounts
                .Where(teacheraccount => teacheraccount.TeacherAccountSex == sex)
                .ToList();

            return teacheraccountsBySex;
        }
        [HttpGet("by-address")]
        public ActionResult<IEnumerable<TeacherAccount>> GetByAddress([FromQuery] string address)
        {
            var teacheraccountsByAddress = _teacheraccountDbContext.Teacheraccounts
                .Where(teacheraccount => teacheraccount.TeacherAccountAddress.Contains(address))
                .ToList();

            return teacheraccountsByAddress;
        }
        [HttpGet("by-phone/{phone}")]
        public async Task<ActionResult<TeacherAccount>> GetByPhone(string phone)
        {
            var teacheraccount = await _teacheraccountDbContext.Teacheraccounts
                .FirstOrDefaultAsync(a => a.TeacherAccountPhone == phone);

            if (teacheraccount == null)
                return NotFound();

            return teacheraccount;
        }
        [HttpGet("by-age")]
        public ActionResult<IEnumerable<TeacherAccount>> GetByAge([FromQuery] int age)
        {
            var today = DateTime.Today;
            var birthdateLimit = today.AddYears(-age);

            var teacheraccountsByAge = _teacheraccountDbContext.Teacheraccounts
                .Where(teacheraccount => teacheraccount.TeacherAccountBirthDate <= birthdateLimit)
                .ToList();

            return teacheraccountsByAge;
        }

        [HttpGet("by-name/{name}")]
        public ActionResult<IEnumerable<TeacherAccount>> GetByName(string name)
        {
            var teacheraccountsByName = _teacheraccountDbContext.Teacheraccounts
                .Where(teacheraccount =>
                    teacheraccount.TeacherAccountUserName.Contains(name) ||
                    teacheraccount.TeacherAccountSurName.Contains(name))
                .ToList();

            return teacheraccountsByName;
        }
        [HttpGet("count")]
        public ActionResult<int> GetTotalTeacherAccountCount()
        {
            var totalTeacherAccounts = _teacheraccountDbContext.Teacheraccounts.Count();
            return totalTeacherAccounts;
        }

    }
}
