using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaLibrary.Services.Audio;
using Microsoft.AspNetCore.Mvc;

namespace MediaLibrary.Controllers.Audio
{
    public class AudioHomeController : Controller
    {
        private readonly IAudioService _service;

        public AudioHomeController(IAudioService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            ViewData["nrofalbums"] = _service.GetAlbumCount();
            ViewData["nrofperformers"] = _service.GetPerformerCount();
            ViewData["nrofsongs"] = _service.GetSongCount();
            return View();
        }
    }
}