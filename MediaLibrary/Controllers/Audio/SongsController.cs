using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.Data;
using MediaLibrary.Models.Audio;
using MediaLibrary.Services.Audio;

namespace MediaLibrary.Controllers.Audio
{
    public class SongsController : Controller
    {
        private readonly IAudioService _service;

        public SongsController(IAudioService service)
        {
            _service = service;
        }

        // GET: Songs
        public IActionResult Index()
        {
            return View(_service.GetSongs());
        }

        // GET: Songs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = _service.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SongID,SongTitle,SongLiryc")] Song song)
        {
            if (ModelState.IsValid)
            {
                _service.AddSong(song);
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = _service.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("SongID,SongTitle,SongLiryc")] Song song)
        {
            if (id != song.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateSong(song);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongID))
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
            return View(song);
        }

        // GET: Songs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = _service.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var song = _service.GetSongById(id);
            _service.DeleteSong(song);
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _service.GetSongById(id) != null;
        }
    }
}
