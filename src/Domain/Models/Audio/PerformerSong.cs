using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Audio
{
    public class PerformerSong : Entity
    {
        private PerformerSong(Guid id, Guid performerID, Guid songID) : base(id)
        {
            PerformerID = performerID;
            SongID = songID;
        }

        [Required]
        public Guid PerformerID { get; private set; }
        public SongPerformer Performer { get; private set; }
        [Required]
        public Guid SongID { get; private set; }
        public Song Song { get; private set; }

        public static PerformerSong Create(Guid performerID, Guid songID)
        {
            var performerSong = new PerformerSong(Guid.NewGuid(), performerID, songID);
            performerSong.IsActive = true;
            return performerSong;
        }
    }
}
