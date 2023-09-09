using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ContactWebAPI.Models;


namespace ContactWebAPI
{
    public class ContactDbContext : DbContext
    {

            public ContactDbContext(DbContextOptions<ContactDbContext> dbContextOptions) : base(dbContextOptions)
            {
                try
                {
                    var databseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                    if (databseCreator != null)
                    {
                        if (!databseCreator.CanConnect()) databseCreator.Create();
                        if (!databseCreator.HasTables()) databseCreator.CreateTables();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            public DbSet<Contact> Contacts { get; set; }
       
    }
}
