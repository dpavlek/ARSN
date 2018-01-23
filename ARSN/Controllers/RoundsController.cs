using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARSN.Models;

namespace ARSN.Controllers
{
    public class RoundsController : Controller
    {
        private readonly DBContext _context;

        public RoundsController(DBContext context)
        {
            _context = context;
        }

        // GET: Rounds
        public async Task<IActionResult> Index()
        {
            var gameCollection=await _context.Round.Include(d => d.GameCollection).ThenInclude(x=>x.HomeTeam).ToListAsync();

            return View(gameCollection);
        }

        // GET: Rounds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Round
                .SingleOrDefaultAsync(m => m.RoundID == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // GET: Rounds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Round
                .SingleOrDefaultAsync(m => m.RoundID == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // POST: Rounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var round = await _context.Round.SingleOrDefaultAsync(m => m.RoundID == id);
            _context.Round.Remove(round);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoundExists(Guid id)
        {
            return _context.Round.Any(e => e.RoundID == id);
        }
    }
}
