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
using MediaLibrary.ViewModels.Audio;

namespace MediaLibrary.Controllers.Audio
{
    public class AlbumsController : Controller
    {
        private readonly IAudioService _service;
        private AlbumDetailsViewModel _detailsViewModel;
        private AlbumEditViewModel _editViewModel;

        public AlbumsController(IAudioService service)
        {
            _service = service;
        }

        // GET: Albums
        public IActionResult Index()
        {
            return View(_service.GetAlbums());
        }

        // GET: Albums/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _service.GetAlbumById(id);
            if(album == null)
            {
                return NotFound();
            }
            if(_detailsViewModel != null)
            {
                _detailsViewModel = null;
            }
            _detailsViewModel = CreateDetailsViewModel(album); 
            return View(_detailsViewModel);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            Album album = new Album();
            if(_editViewModel != null)
            {
                _editViewModel = null;
            }
            _editViewModel = CreateEditViewModel(album);
            return View(_editViewModel);
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int AudioFormatID, [Bind("AlbumID,AlbumTitle,NrOfDiscs")] Album album)
        {
            if (ModelState.IsValid)
            {
                album.AlbumFormat = _service.GetFormatById(AudioFormatID);
                _service.AddAlbum(album);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _service.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            if (_editViewModel != null)
            {
                _editViewModel = null;
            }
            _editViewModel = CreateEditViewModel(album);
            return View(_editViewModel);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, int AudioFormatID, [Bind("AlbumID,AlbumTitle,NrOfDiscs")] Album album)
        {
            if (id != album.AlbumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    album.AlbumFormat = _service.GetFormatById(AudioFormatID);
                    _service.UpdateAlbum(album);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumID))
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
            return View(album);
        }

        // GET: Albums/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _service.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            if (_detailsViewModel != null)
            {
                _detailsViewModel = null;
            }
            _detailsViewModel = CreateDetailsViewModel(album);
            return View(_detailsViewModel);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var album = _service.GetAlbumById(id);
            _service.DeleteAlbum(album);
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _service.GetAlbumById(id) != null;
        }

        private AlbumEditViewModel CreateEditViewModel(Album album)
        {
            var vm = new AlbumEditViewModel();
            if (album != null)
            {
                vm.Album = album;
                vm.AudioFormats = _service.GetFormatsToViews();
                if (album.AlbumFormat != null)
                {
                    vm.AudioFormatID = album.AlbumFormat.AudioFormatID;
                }
                var songs = _service.GetSongsOfAlbum(album);
                foreach (var item in songs)
                {
                    vm.Songs.Add(new AlbumSongViewModel { SongOfAlbum = item });
                }
            }
            return vm;
        }

        private AlbumDetailsViewModel CreateDetailsViewModel(Album album)
        {
            var model = new AlbumDetailsViewModel();
            if(album != null)
            {
                model.Album = album;
                model.Details = _service.GetSongsOfAlbum(album);
            }
            return model;
        }
    }
}
