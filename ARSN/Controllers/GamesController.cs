using ARSN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Controllers
{
    public class GamesController : Controller
    {
        private readonly DBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GamesController(DBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Game
                .Include(s=>s.HomeTeam)
                .Include(d=>d.AwayTeam)
                .ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(s => s.HomeTeam)
                .Include(d => d.AwayTeam)
                .SingleOrDefaultAsync(m => m.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public async Task<IActionResult> Create()
        {
            PopulateHomeTeamsDropDownList();
            PopulateAwayTeamsDropDownList();
            var user = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated && user.Verified)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game)
        {
            if (ModelState.IsValid && Request.Form["HomeTeam"]!="" && Request.Form["AwayTeam"] != "")
            {
               Guid HomeTeamID = new Guid(Request.Form["HomeTeam"]);
               var homeTeam = await _context.Team
                    .SingleOrDefaultAsync(m => m.TeamID == HomeTeamID);
                game.HomeTeam = homeTeam;
               // System.IO.File.WriteAllText(@"D:\home.txt", homeTeam.Name);

                Guid AwayTeamID = new Guid(Request.Form["AwayTeam"]);
                var awayTeam = await _context.Team
                     .SingleOrDefaultAsync(m => m.TeamID == AwayTeamID);
                game.AwayTeam = awayTeam;
                //System.IO.File.WriteAllText(@"D:\away.txt", awayTeam.Name);

                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateHomeTeamsDropDownList(game.HomeTeam);
            PopulateAwayTeamsDropDownList(game.AwayTeam);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.Include(d=>d.HomeTeam).Include(d=>d.AwayTeam).AsNoTracking().SingleOrDefaultAsync(m => m.GameID == id);
            if (game == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated && user.Verified)
            {
                return View(game);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GameID,Type,HomeResult,AwayResult,Winner")] Game game)
        {
            if (id != game.GameID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Int32.TryParse(game.HomeResult, out int HomeResult);
                    Int32.TryParse(game.AwayResult, out int AwayResult);

                    if (HomeResult > AwayResult)
                        game.Winner = "Domaći";
                    else game.Winner = "Gosti";
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Rounds");
            }

            return View(game);
        }

        private void PopulateHomeTeamsDropDownList(object selectedHomeTeam=null)
        {
            var homeTeamQuery = from d in _context.Team 
                                orderby d.Name 
                                select d;
            ViewBag.HomeTeam = new SelectList(homeTeamQuery.AsNoTracking(), "TeamID", "Name", selectedHomeTeam);
        }
        private void PopulateAwayTeamsDropDownList(object selectedAwayTeam = null)
        {
            var awayTeamQuery = from d in _context.Team
                                orderby d.Name
                                select d;
            ViewBag.AwayTeam = new SelectList(awayTeamQuery.AsNoTracking(), "TeamID", "Name", selectedAwayTeam);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .SingleOrDefaultAsync(m => m.GameID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game = await _context.Game.SingleOrDefaultAsync(m => m.GameID == id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(Guid id)
        {
            return _context.Game.Any(e => e.GameID == id);
        }

 
    }
}
