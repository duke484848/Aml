namespace Aml.Data
{
    using Aml.Models.Api.CompanyController;
    using Microsoft.EntityFrameworkCore;

    public class AmlContext : DbContext
    {
        public AmlContext()
          : base()
        {
        }

        public AmlContext(DbContextOptions<AmlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }

        public virtual DbSet<Notification> Notification { get; set; }
    }
}
