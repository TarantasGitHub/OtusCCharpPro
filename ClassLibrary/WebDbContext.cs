using ClassLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary
{
    /*
     * dotnet ef migrations add InitialCreate --context WebDbContext --output-dir Migrations\PostgreSqlMigrations -p ClassLibrary\ClassLibrary.csproj -s WebApi\WebApi.csproj
     * 
     * Done. To undo this action, use 'ef migrations remove'
     * */
    public class WebDbContext : DbContext
    {
        static WebDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public WebDbContext() : base() { }

        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conn = "Host=localhost;Database=WebApiDb;Username=admin;Password=admin;Port=5433";
                optionsBuilder.UseNpgsql(conn/*, sql => sql.MigrationsAssembly("YourRest.Producer.Infrastructure")*/)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
