using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ARSN.Models
{
    public class DBContext : DbContext
    {
        public DBContext():base()
        {
        }

        public DBContext(DbContextOptions<DBContext> options):base(options){ }
        public DbSet<Team> Team{get;set;}
        public DbSet<Organizer> Organizer { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<Game> Game { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}
