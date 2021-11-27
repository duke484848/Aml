namespace Aml.Data
{
    using Aml.Models.Api.CompanyController;
    using Microsoft.EntityFrameworkCore;

    public class AmlContext : DbContext
    {
        public AmlContext(DbContextOptions<AmlContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }

        public DbSet<Notification> Notification { get; set; }
    }
}
