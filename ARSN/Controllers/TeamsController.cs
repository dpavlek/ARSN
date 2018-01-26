using ARSN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeamsController(DBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Team.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public async Task<IActionResult> Create()
        {
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

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamID,Name,Organisation,Email,TrainerName")] Team team)
        {
            if (ModelState.IsValid)
            {
                team.TeamID = Guid.NewGuid();
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated && user.Verified)
            {
                return View(team);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TeamID,Name,Organisation,Email,TrainerName")] Team team)
        {
            if (id != team.TeamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamID))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated && user.Verified)
            {
                return View(team);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var homeTeam = await _context.Game
                .Include(d => d.HomeTeam)
                .SingleOrDefaultAsync(d => d.HomeTeam.TeamID == id);
            var awayTeam = await _context.Game
                    .Include(d => d.AwayTeam)
                    .SingleOrDefaultAsync(d => d.AwayTeam.TeamID == id);
            if (homeTeam==null && awayTeam==null)
            {
                var team = await _context.Team
                    .SingleOrDefaultAsync(m => m.TeamID == id);
                _context.Team.Remove(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Error", "Odabrani tim je vec u natjecanju, nije ga moguce obrisati!");
            }
            var teamShow = await _context.Team
               .SingleOrDefaultAsync(m => m.TeamID == id);
            return View(teamShow);
        }

        private bool TeamExists(Guid id)
        {
            return _context.Team.Any(e => e.TeamID == id);
        }
    }
}
