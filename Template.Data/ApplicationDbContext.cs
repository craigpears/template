using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Data.Entities;

namespace Template.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<QuoteLine> QuoteLines { get; set; }
    }
}
