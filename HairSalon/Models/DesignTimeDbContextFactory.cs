using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Salon.Models
{
    public class SalonContextFactory : IDesignTimeDbContextFactory<SalonContext>
    {
        SalonContext IDesignTimeDbContextFactory<SalonContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SalonContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseMySql(connectionString);

            return new SalonContext(builder.Options);
        }
    }
}