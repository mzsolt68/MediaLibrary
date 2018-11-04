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
using MediaLibrary.ViewModels.Audio;

namespace MediaLibrary.Controllers.Audio
{
    public class SongsController : Controller
    {
        private readonly IAudioService _service;
        private SongEditViewModel _editViewModel;
        private SongDetailsViewModel _detailViewModel;

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
            if(_detailViewModel != null)
            {
                _detailViewModel = null;
            }
            _detailViewModel = CreateDetailsViewModel(song);
            return View(_detailViewModel);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            var song = new Song();
            if (_editViewModel != null)
            {
                _editViewModel = null;
            }
            _editViewModel = CreateEditViewModel(song);
            return View(_editViewModel);
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Song, Performers")] SongEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddSong(vm.Song, vm.Performers);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
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
            if(_editViewModel != null)
            {
                _editViewModel = null;
            }
            _editViewModel = CreateEditViewModel(song);
            return View(_editViewModel);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Song, Performers")] SongEditViewModel vm)
        {
            if (id != vm.Song.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateSong(vm.Song, vm.Performers);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(vm.Song.SongID))
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
            return View(vm);
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
            if(_detailViewModel != null)
            {
                _detailViewModel = null;
            }
            _detailViewModel = CreateDetailsViewModel(song);
            return View(_detailViewModel);
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

        private SongEditViewModel CreateEditViewModel(Song song)
        {
            SongEditViewModel vm = new SongEditViewModel();
            vm.Song = song;
            vm.PerformerList = _service.GetPerformersToViews();
            if(song != null)
            {
                var performers = _service.GetPerformersOfSong(song).ToList();
                foreach (var item in performers)
                {
                    vm.Performers.Add(new SongPerformerViewModel { Performer = item });
                }
            }
            return vm;
        }

        private SongDetailsViewModel CreateDetailsViewModel(Song song)
        {
            var vm = new SongDetailsViewModel();
            if (song != null)
            {
                vm.Song = song;
                vm.AlbumsOfSong = _service.GetAlbumsOfSong(song);
                vm.PerformersOfSong = _service.GetPerformersOfSong(song);
            }
            return vm;
        }

        [HttpPost]
        public IActionResult AddPerformer(int id)
        {
            var performer = _service.GetPerformerById(id);
            if (performer != null)
            {
                SongPerformerViewModel vm = new SongPerformerViewModel { Performer = performer };
                return PartialView("Audio/PerformerListPartial", vm);
            }
            return NotFound();
        }
    }
}
