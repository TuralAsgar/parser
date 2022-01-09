using Microsoft.EntityFrameworkCore;
using Parser.Models;

namespace Parser.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Meta>? Metas { get; set; }
    public DbSet<Report>? Reports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meta>().ToTable("meta");
        modelBuilder.Entity<Report>().ToTable("report");
    } 
}