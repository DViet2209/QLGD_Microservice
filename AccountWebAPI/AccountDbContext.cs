using AccountWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace AccountWebAPI
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext>dbContextOptions) : base(dbContextOptions)
        {
            try 
            {
                var databseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databseCreator != null ) 
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
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TeacherAccount> Teacheraccounts { get; set; }
    }
}
