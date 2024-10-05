using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TransaccionesApp.Models;

namespace TransaccionesApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

   public DbSet<TransaccionesApp.Models.Remesa> DataRemesa { get;set;}
}
