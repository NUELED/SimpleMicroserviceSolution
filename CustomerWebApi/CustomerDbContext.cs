using CustomerWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerWebApi
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext( DbContextOptions<CustomerDbContext> options) : base(options) 
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


        public DbSet<Customer> Customers { get; set; }  
    }
}
