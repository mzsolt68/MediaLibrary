using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.Entities.Models.Audio;
using MediaLibrary.Services.Audio;
using MediaLibrary.ViewModels.Audio;

namespace MediaLibrary.Controllers.Audio
{
    public class PerformersController : Controller
    {
        private readonly IAudioService _service;
        private readonly PerformerDetailsViewModel _detailsViewModel;

        public PerformersController(IAudioService service)
        {
            _service = service;
        }
        // GET: Performers
        public IActionResult Index()
        {
            return View(_service.GetPerformers());
        }

        // GET: Performers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var performer = _service.GetPerformerById(id);
            if (performer == null)
            {
                return NotFound();
            }
            return View(CreateDetailsViewModel(performer));
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
        public IActionResult Create([Bind("PerformerID,PerformerName")] SongPerformer performer)
        {
            if (ModelState.IsValid)
            {
                _service.AddPerformer(performer);
                return RedirectToAction(nameof(Index));
            }
            return View(performer);
        }

        // GET: Performers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = _service.GetPerformerById(id);
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
        public IActionResult Edit(int id, [Bind("PerformerID,PerformerName")] SongPerformer performer)
        {
            if (id != performer.PerformerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdatePerformer(performer);
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = _service.GetPerformerById(id);
            if (performer == null)
            {
                return NotFound();
            }

            return View(CreateDetailsViewModel(performer));
        }

        // POST: Performers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var performer = _service.GetPerformerById(id);
            _service.DeletePerformer(performer);
            return RedirectToAction(nameof(Index));
        }

        private bool PerformerExists(int id)
        {
            return _service.GetPerformerById(id) != null;
        }

        private PerformerDetailsViewModel CreateDetailsViewModel(SongPerformer performer)
        {
            var model = new PerformerDetailsViewModel();
            if (performer != null)
            {
                model.Performer = performer;
                model.Songs = _service.SongsOfPerformer(performer);
            }
            return model;
        }
    }
}
