using Kingdom.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KingdomWebApp.Models;

namespace KingdomWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SupplyOwner> SupplyOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplyOwner>()
                .HasOne(s => s.Owner)
                .WithMany(o => o.SupplyOwners) // Assuming 'SupplyOwners' is a collection navigation property in the 'Owner' entity
                .HasForeignKey(s => s.OwnerId); // Assuming 'OwnerId' is a foreign key property in the 'SupplyOwner' entity

            base.OnModelCreating(modelBuilder);
        }

    }


}
