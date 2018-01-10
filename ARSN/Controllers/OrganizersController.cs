using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARSN.Models;
using CryptoHelper;

namespace ARSN.Controllers
{
    public class OrganizersController : Controller
    {
        private readonly DBContext _context;

        public OrganizersController(DBContext context)
        {
            _context = context;
        }

        // GET: Organizers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizer.ToListAsync());
        }

        // GET: Organizers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizer
                .SingleOrDefaultAsync(m => m.OrganizerID == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // GET: Organizers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizerID,Name,Surname,Email,BirthDate,Organisation,PhoneNumber,Gender,Password,Verified")] Organizer organizer)
        {
            if (ModelState.IsValid)
            {
                organizer.Password = Crypto.HashPassword(organizer.Password);
                organizer.OrganizerID = Guid.NewGuid();
                _context.Add(organizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizer);
        }

        // GET: Organizers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizer.SingleOrDefaultAsync(m => m.OrganizerID == id);
            if (organizer == null)
            {
                return NotFound();
            }
            return View(organizer);
        }

        // POST: Organizers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrganizerID,Name,Surname,Email,BirthDate,Organisation,PhoneNumber,Gender,Verified")] Organizer organizer)
        {
            if (id != organizer.OrganizerID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerExists(organizer.OrganizerID))
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
            return View(organizer);
        }

        // GET: Organizers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizer
                .SingleOrDefaultAsync(m => m.OrganizerID == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // POST: Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var organizer = await _context.Organizer.SingleOrDefaultAsync(m => m.OrganizerID == id);
            _context.Organizer.Remove(organizer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizerExists(Guid id)
        {
            return _context.Organizer.Any(e => e.OrganizerID == id);
        }
    }
}
