namespace SeatsProject.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SeatsProject.Models;

    public class SeatsProjectContext : DbContext
    {
        public DbSet<Sede> Sedi { get; set; }

        public SeatsProjectContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Prenotazioni> Prenotazioni { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
