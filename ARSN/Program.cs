using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ARSN.Models;

namespace ARSN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TO DO: check if DB works
            /*var context = new DBContext();
            var t = new Team();
            t.Name = "Jozo";
            t.TeamID = "14561456";
            using (var context1 = new DBContext())
            {
                context.Team.Add(t);
                context.SaveChanges();
            }*/
            BuildWebHost(args).Run();
            
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
