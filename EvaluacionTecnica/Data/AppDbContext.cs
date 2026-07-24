using EvaluacionTecnica.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionTecnica.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
    }
}
