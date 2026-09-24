namespace TextForge.Data;

using Microsoft.EntityFrameworkCore;
using TextForge.Text;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Text> Texts { get; set; }
}
