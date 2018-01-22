using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ARSN.Models;

namespace ARSN.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly DBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompetitionsController(DBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Competition.ToListAsync());
        }

        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .SingleOrDefaultAsync(m => m.CompetitionID == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // GET: Competitions/Create
        public async Task<IActionResult> Create()
        {
            PopulateHomeTeamsDropDownList();
            PopulateAwayTeamsDropDownList();

            var user = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                if (user.Verified)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Competitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetitionID,Name,SportType,CompetitionBegin,CompetitionEnd")] Competition competition, string button, string submit)
        {
           
            //Guid HomeTeamID = new Guid(Request.Form["HomeTeam"]);
            //var homeTeam = await _context.Team
            //.SingleOrDefaultAsync(m => m.TeamID == HomeTeamID);
            //game.HomeTeam = homeTeam;

            // Guid AwayTeamID = new Guid(Request.Form["AwayTeam"]);
            // var awayTeam = await _context.Team
            // .SingleOrDefaultAsync(m => m.TeamID == AwayTeamID);
            //game.AwayTeam = awayTeam;
            //if (button == "Dodaj timove u natjecanje")
            // {
            //System.IO.File.WriteAllText(@"E:\home.txt", homeTeam.Name);
            //System.IO.File.WriteAllText(@"E:\away.txt", awayTeam.Name);
            // }
            // if (button == "Dodaj timove") return RedirectToAction("Create", "Teams");

            if (ModelState.IsValid)
            {
                string tempTeams = Request.Form["TeamList"];
                string[] lines = tempTeams.Split(
                        new[] { "\r\n", "\r", "\n","-" },
                        StringSplitOptions.None
                    );
                //var game = await _context.Game
                   //  .Include(s => s.HomeTeam)
                    // .Include(d => d.AwayTeam)
                    // .SingleOrDefaultAsync(m => m.GameID == id);
                foreach(var team in lines)
                {
                    var HomeTeam = await _context.Team.SingleOrDefaultAsync(d => d.Name == team);
                }
                System.IO.File.AppendAllLines(@"D:\request.txt", lines);
                    _context.Add(competition);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }

            return View(competition);        
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition.SingleOrDefaultAsync(m => m.CompetitionID == id);
            if (competition == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated && user.Verified)
            {
                return View(competition);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Competitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompetitionID,Name,SportType,CompetitionBegin,CompetitionEnd")] Competition competition)
        {
            if (id != competition.CompetitionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.CompetitionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .SingleOrDefaultAsync(m => m.CompetitionID == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var competition = await _context.Competition.SingleOrDefaultAsync(m => m.CompetitionID == id);
            _context.Competition.Remove(competition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(Guid id)
        {
            return _context.Competition.Any(e => e.CompetitionID == id);
        }

        private void PopulateHomeTeamsDropDownList(object selectedHomeTeam = null)
        {
            var homeTeamQuery = from d in _context.Team
                                orderby d.Name
                                select d;
            ViewBag.HomeTeam = new SelectList(homeTeamQuery.AsNoTracking(), "Name", "Name", selectedHomeTeam);
        }
        private void PopulateAwayTeamsDropDownList(object selectedAwayTeam = null)
        {
            var awayTeamQuery = from d in _context.Team
                                orderby d.Name
                                select d;
            ViewBag.AwayTeam = new SelectList(awayTeamQuery.AsNoTracking(), "Name", "Name", selectedAwayTeam);
        }

    }
}
