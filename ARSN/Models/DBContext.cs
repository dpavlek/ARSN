using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ARSN.Models
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructors

        public DBContext():base()
        {
        }

        #endregion Constructors

        #region Properties
        public DBContext(DbContextOptions<DBContext> options):base(options){ }
        public DbSet<Team> Team{get;set;}
        public DbSet<Competition> Competition { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Round> Round { get; set; }

        #endregion Properties

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Competition>().Property(x => x.CompetitionID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Game>().Property(x => x.GameID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Team>().Property(x => x.TeamID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Round>().Property(x => x.RoundID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Round>()
                .HasOne(p => p.Competition)
                .WithMany(b => b.RoundCollection)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<Game>()
              .HasOne(p => p.Round)
              .WithMany(b => b.GameCollection)
              .Metadata.DeleteBehavior=DeleteBehavior.Restrict;

            modelBuilder.Entity<Competition>()
             .HasOne(p => p.ApplicationUser)
             .WithMany(b => b.Competitions)
             .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
        #endregion Methods
    }
}
