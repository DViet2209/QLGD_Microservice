using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using CourseWebAPI.Models;

namespace CourseWebAPI
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> dbContextOptions) : base(dbContextOptions)
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
        public DbSet<Course> Courses { get; set; }
    }
}
