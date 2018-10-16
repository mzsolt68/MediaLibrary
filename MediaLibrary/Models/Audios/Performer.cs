using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLibrary.Models.Audios
{
    public class Performer
    {
        public int PerformerID { get; set; }
        public string PerformerName { get; set; }

        public virtual ICollection<Song> SongsOfPerformer { get; set; }
    }
}
