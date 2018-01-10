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
using Microsoft.EntityFrameworkCore;

namespace ARSN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TO DO: check if DB works
            //var context = new DBContext();
            /* var t = new Team();
             t.Name = "Pero";
             t.TeamID = "111";
             using (var context1 = new DBContext())
             {
                 context1.Database.OpenConnection();
               context1.Team.Add(t);

               context1.SaveChanges();
             }*/
            using (var context = new DBContext())
            {
                #region Adding Values Into Database

                Organizer OrganizerObject = new Organizer
                {
                    Name = "Ime",
                    Surname = "Prezime",
                    Email = "mail@gmail.com",
                    BirthDate = new DateTime(1991, 03, 25),
                    Organisation = "Organizacija",
                    PhoneNumber = "09969854645",
                    Gender = "M"
                };
                context.Organizer.Add(OrganizerObject);
                context.SaveChanges();

                #endregion Adding Values Into Database
            }
            BuildWebHost(args).Run();
            
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
