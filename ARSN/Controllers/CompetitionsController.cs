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
        public async Task<IActionResult> Create([Bind("CompetitionID,Name,SportType,CompetitionBegin,CompetitionEnd")] Competition competition)
        {
            PopulateHomeTeamsDropDownList();
            PopulateAwayTeamsDropDownList();
            System.IO.File.AppendAllText(@"D:\button.txt", Request.Form["button"]);
            if (ModelState.IsValid)
            {
                string tempTeams = Request.Form["TeamList"];
                string[] lines = tempTeams.Split(
                        new[] { "\r\n", "\r", "\n","-" },
                        StringSplitOptions.None
                    ); 
                //Randomizing teams
                for(int i=0;i<lines.Length;i++)
                    System.IO.File.AppendAllText(@"D:\before.txt", lines[i]);
                var List = lines.ToList();
                List.Shuffle();
                foreach(var temp in List)
                {
                    System.IO.File.AppendAllText(@"D:\after.txt", temp);
                }
                Team HomeTeam =null, AwayTeam=null;
                List<Game> ListGames=new List<Game>();
                Round FirstRound = new Round
                {
                    Name = "1.kolo",
                    Finished = false
                };

                for (int i = 0; i < lines.Length-1; i += 2)
                {
                    HomeTeam = await _context.Team.SingleAsync(d => d.Name == lines[i]);
                    AwayTeam = await _context.Team.SingleAsync(d => d.Name == lines[i+1]);
                    Game NewGame = new Game
                    {
                        HomeTeam = HomeTeam,
                        AwayTeam = AwayTeam,
                        Type = competition.SportType
                    };

                    _context.Game.Add(NewGame);
                    await _context.SaveChangesAsync();
                    ListGames.Add(NewGame);
                    HomeTeam = null;
                    AwayTeam = null;
                }
               
                FirstRound.GameCollection = ListGames ;
                 _context.Add(FirstRound);
                await _context.SaveChangesAsync();
                List<Round> ListRound = new List<Round>
                {
                    FirstRound
                };
                competition.RoundCollection=ListRound;
                //competition.ApplicationUser = _userManager.GetUserAsync(SecurityStampRefreshingPrincipalContext);
                //System.IO.File.WriteAllText(@"D:\home.txt", competition.ApplicationUser.Gender);
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
