using HudsonLearning.Entities;
using Microsoft.EntityFrameworkCore;

namespace HudsonLearning.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
    }
}
