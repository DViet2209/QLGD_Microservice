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
    public class AccountController : ControllerBase
    {

        private readonly AccountDbContext _accountDbContext;
        public AccountController(AccountDbContext accountDbContext) 
        {
            _accountDbContext = accountDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccount() 
        {
            return _accountDbContext.Accounts;
        }
        [HttpGet("{accountId:int}")]
        public async Task<ActionResult<Account>> GetById(int accountId) 
        {
            var account = await _accountDbContext.Accounts.FindAsync(accountId);
            return account;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Account account)
        {
            var outputParam = new SqlParameter("@output", System.Data.SqlDbType.NVarChar, 100);
            outputParam.Direction = System.Data.ParameterDirection.Output;

            await _accountDbContext.Database
                .ExecuteSqlRawAsync("EXEC dbo.pr_AccountUserId @output OUTPUT", outputParam);

            var newAccountID = outputParam.Value as string;

            account.AccountUserId = newAccountID;
            await _accountDbContext.Accounts.AddAsync(account);
            await _accountDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Account account)
        {
            _accountDbContext.Accounts.Update(account);
            await _accountDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{accountId:int}")]
        public async Task<ActionResult<Account>> Delete(int accountId)
        {
            var account = await _accountDbContext.Accounts.FindAsync(accountId);
            _accountDbContext.Accounts.Remove(account);
            await _accountDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Account>> SearchAccounts([FromQuery] string keyword)
        {
            var matchingAccounts = _accountDbContext.Accounts
                .Where(account =>
                    account.AccountUserId.Contains(keyword) ||
                    account.AccountUserName.Contains(keyword) ||
                    account.AccountSurName.Contains(keyword))
                .ToList();

            return matchingAccounts;
        }

        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<Account>> GetByUsername(string username)
        {
            var account = await _accountDbContext.Accounts
                .FirstOrDefaultAsync(a => a.AccountUserName == username);

            if (account == null)
                return NotFound();

            return account;
        }

        [HttpGet("by-surname/{surname}")]
        public async Task<ActionResult<Account>> GetBySurname(string surname)
        {
            var account = await _accountDbContext.Accounts
                .FirstOrDefaultAsync(a => a.AccountSurName == surname);

            if (account == null)
                return NotFound();

            return account;
        }

        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<Account>> GetByEmail(string email)
        {
            var account = await _accountDbContext.Accounts
                .FirstOrDefaultAsync(a => a.AccountEmail == email);

            if (account == null)
                return NotFound();

            return account;
        }

        [HttpGet("by-birthdate-range")]
        public ActionResult<IEnumerable<Account>> GetByBirthdateRange(
            [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var accountsInDateRange = _accountDbContext.Accounts
                .Where(account => account.AccountBirthDate >= fromDate && account.AccountBirthDate <= toDate)
                .ToList();

            return accountsInDateRange;
        }

        [HttpGet("by-sex/{sex}")]
        public ActionResult<IEnumerable<Account>> GetBySex(string sex)
        {
            var accountsBySex = _accountDbContext.Accounts
                .Where(account => account.AccountSex == sex)
                .ToList();

            return accountsBySex;
        }

        [HttpGet("by-address")]
        public ActionResult<IEnumerable<Account>> GetByAddress([FromQuery] string address)
        {
            var accountsByAddress = _accountDbContext.Accounts
                .Where(account => account.AccountAddress.Contains(address))
                .ToList();

            return accountsByAddress;
        }

        [HttpGet("by-phone/{phone}")]
        public async Task<ActionResult<Account>> GetByPhone(string phone)
        {
            var account = await _accountDbContext.Accounts
                .FirstOrDefaultAsync(a => a.AccountPhone == phone);

            if (account == null)
                return NotFound();

            return account;
        }

        [HttpGet("by-age")]
        public ActionResult<IEnumerable<Account>> GetByAge([FromQuery] int age)
        {
            var today = DateTime.Today;
            var birthdateLimit = today.AddYears(-age);

            var accountsByAge = _accountDbContext.Accounts
                .Where(account => account.AccountBirthDate <= birthdateLimit)
                .ToList();

            return accountsByAge;
        }

        [HttpGet("by-name/{name}")]
        public ActionResult<IEnumerable<Account>> GetByName(string name)
        {
            var accountsByName = _accountDbContext.Accounts
                .Where(account =>
                    account.AccountUserName.Contains(name) ||
                    account.AccountSurName.Contains(name))
                .ToList();

            return accountsByName;
        }

        [HttpGet("count")]
        public ActionResult<int> GetTotalAccountCount()
        {
            var totalAccounts = _accountDbContext.Accounts.Count();
            return totalAccounts;
        }


    }

}
