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
    }

}
