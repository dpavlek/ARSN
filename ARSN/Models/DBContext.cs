using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ARSN.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options):base(options){ }
        public DbSet<Team> Team{get;set;}
        public DbSet<>
    }
}
