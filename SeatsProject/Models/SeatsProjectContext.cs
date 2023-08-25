using Microsoft.EntityFrameworkCore;
using SeatsProject.Models;
namespace SeatsProject.Models
{
    using Microsoft.EntityFrameworkCore;

    public class SeatsProjectContext : DbContext
    {
        public DbSet<Sede> Sedi { get; set; }

        public SeatsProjectContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SeatsProject.Models.Prenotazioni>? Prenotazioni { get; set; }
    }
}
