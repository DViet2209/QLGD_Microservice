using ContactWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ContactWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactDbContext _contactDbContext;
        public ContactController(ContactDbContext contactDbContext)
        {
            _contactDbContext = contactDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContact()
        {
            return _contactDbContext.Contacts;
        }
        [HttpGet("{contactId:int}")]
        public async Task<ActionResult<Contact>> GetById(int contactId)
        {
            var contact = await _contactDbContext.Contacts.FindAsync(contactId);
            return contact;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Contact contact)
        {
            await _contactDbContext.Contacts.AddAsync(contact);
            await _contactDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{contactId:int}")]
        public async Task<ActionResult<Contact>> Delete(int contactId)
        {
            var contact = await _contactDbContext.Contacts.FindAsync(contactId);
            _contactDbContext.Contacts.Remove(contact);
            await _contactDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("count")]
        public ActionResult<int> GetTotalContactCount()
        {

            var totalContacts = _contactDbContext.Contacts.Count();
            return totalContacts;

        }

    }
}
