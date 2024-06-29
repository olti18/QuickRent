using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickRent.Areas.Identity.Data;
using QuickRent.Models;

namespace QuickRent.Data;

public class QuickDbContext : IdentityDbContext<ApplicationUser>
{
    public QuickDbContext(DbContextOptions<QuickDbContext> options)
        : base(options)
    {

    }
    public DbSet<Car> Cars { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
