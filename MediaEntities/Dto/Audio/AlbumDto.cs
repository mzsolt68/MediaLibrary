using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Entities.Dto.Audio
{
    public class AlbumDto
    {
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }
        public int Nr_of_discs { get; set; }
        public int Nr_of_tracks { get; set; }
    }
}
