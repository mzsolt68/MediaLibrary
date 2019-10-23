using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaEntities.Data;
using MediaEntities.Models.Audio;
using MediaLibrary.Services.Audio;

namespace MediaLibrary.Controllers.Audio
{
    public class AudioFormatsController : Controller
    {
        private readonly IAudioService _service;

        public AudioFormatsController(IAudioService service)
        {
            _service = service;
        }

        // GET: AudioFormats
        public IActionResult Index()
        {
            return View(_service.GetFormats());
        }

        // GET: AudioFormats/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFormat = _service.GetFormatById(id);
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
        public IActionResult Create([Bind("AudioFormatID,AudioFormatName")] AudioFormat audioFormat)
        {
            if (ModelState.IsValid)
            {
                _service.AddFormat(audioFormat);
                return RedirectToAction(nameof(Index));
            }
            return View(audioFormat);
        }

        // GET: AudioFormats/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFormat = _service.GetFormatById(id);
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
        public IActionResult Edit(int id, [Bind("AudioFormatID,AudioFormatName")] AudioFormat audioFormat)
        {
            if (id != audioFormat.AudioFormatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateFormat(audioFormat);
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFormat = _service.GetFormatById(id);
            if (audioFormat == null)
            {
                return NotFound();
            }

            return View(audioFormat);
        }

        // POST: AudioFormats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var audioFormat = _service.GetFormatById(id);
            _service.DeleteFormat(audioFormat);
            return RedirectToAction(nameof(Index));
        }

        private bool AudioFormatExists(int id)
        {
            return _service.GetFormatById(id) != null;
        }
    }
}
