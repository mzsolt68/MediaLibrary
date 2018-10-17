using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;

namespace MediaLibrary.Controllers.Audio
{
    public class PerformersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Performers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Performers.ToListAsync());
        }

        // GET: Performers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers
                .FirstOrDefaultAsync(m => m.PerformerID == id);
            if (performer == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        // GET: Performers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Performers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerformerID,PerformerName")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performer);
        }

        // GET: Performers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers.FindAsync(id);
            if (performer == null)
            {
                return NotFound();
            }
            return View(performer);
        }

        // POST: Performers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformerID,PerformerName")] Performer performer)
        {
            if (id != performer.PerformerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformerExists(performer.PerformerID))
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
            return View(performer);
        }

        // GET: Performers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers
                .FirstOrDefaultAsync(m => m.PerformerID == id);
            if (performer == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        // POST: Performers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performer = await _context.Performers.FindAsync(id);
            _context.Performers.Remove(performer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformerExists(int id)
        {
            return _context.Performers.Any(e => e.PerformerID == id);
        }
    }
}
