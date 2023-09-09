using TimetableWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace TimetableWebAPI
{
    public class TimetableDbContext : DbContext
    {
        public TimetableDbContext(DbContextOptions<TimetableDbContext> dbContextOptions) : base(dbContextOptions)
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
        public DbSet<TimeTable> Timetables { get; set; }
    }
}
