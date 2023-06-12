using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProductWebApi.Models;

namespace ProductWebApi
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) 
        {

            try
            {
                //The code below is to create a database if the database does not exist.

                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        public DbSet<Product> Products { get; set; }    
    }
}
