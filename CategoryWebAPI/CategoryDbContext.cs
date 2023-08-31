using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using CategoryWebAPI.Models;


namespace CategoryWebAPI
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> dbContextOptions) : base(dbContextOptions)
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
        public DbSet<Category> Categorys { get; set; }
    }
}
