using Microsoft.EntityFrameworkCore;
using KingdomWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomWebApp.Scraper.Data
{
    public class ScraperDBContext : DbContext
    {
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EI2TOGP\\SQLEXPRESS;Initial Catalog=Kingdoms;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
