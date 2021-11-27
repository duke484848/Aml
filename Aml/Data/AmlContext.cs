namespace Aml.Data
{
    using Microsoft.EntityFrameworkCore;

    public class AmlContext : DbContext
    {
        public AmlContext(DbContextOptions<AmlContext> options)
            : base(options)
        {
        }

        public DbSet<Aml.Models.Api.Company> Company { get; set; }
    }
}
