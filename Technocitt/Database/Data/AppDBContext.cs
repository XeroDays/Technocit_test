using Microsoft.EntityFrameworkCore;
using Technocitt.Database.Tables;
using Technocitt.mydb;

namespace Technocitt.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        } 
        public DbSet<CustomUser> TechnoUsers { get; set; } 
        public DbSet<CustomProperty> TechnoProperties { get; set; }
    }

}
