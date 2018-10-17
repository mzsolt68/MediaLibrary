using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;

namespace MediaLibrary.Controllers
{
    public class AudioFormatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AudioFormatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AudioFormats
        public async Task<IActionResult> Index()
        {
            return View(await _context.AudioFormats.ToListAsync());
        }

        // GET: AudioFormats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFormat = await _context.AudioFormats
                .FirstOrDefaultAsync(m => m.AudioFormatID == id);
            if (audioFormat == null)
            {
                return NotFound();
            }

            return View(audioFormat);
        }

        // GET: AudioFormats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AudioFormats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AudioFormatID,AudioFormatName")] AudioFormat audioFormat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audioFormat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audioFormat);
        }

        // GET: AudioFormats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFormat = await _context.AudioFormats.FindAsync(id);
            if (audioFormat == null)
            {
                return NotFound();
            }
            return View(audioFormat);
        }

        // POST: AudioFormats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AudioFormatID,AudioFormatName")] AudioFormat audioFormat)
        {
            if (id != audioFormat.AudioFormatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audioFormat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudioFormatExists(audioFormat.AudioFormatID))
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
            return View(audioFormat);
        }

        // GET: AudioFormats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFormat = await _context.AudioFormats
                .FirstOrDefaultAsync(m => m.AudioFormatID == id);
            if (audioFormat == null)
            {
                return NotFound();
            }

            return View(audioFormat);
        }

        // POST: AudioFormats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audioFormat = await _context.AudioFormats.FindAsync(id);
            _context.AudioFormats.Remove(audioFormat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudioFormatExists(int id)
        {
            return _context.AudioFormats.Any(e => e.AudioFormatID == id);
        }
    }
}
