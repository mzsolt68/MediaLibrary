using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SharedKernel;

namespace Domain.Models.Audio
{
    public class SongPerformer : Entity
    {
        private HashSet<PerformerSong> _songs;

        private SongPerformer(Guid id, string performerName) : base(id)
        {
            PerformerName = performerName;
            _songs = new HashSet<PerformerSong>();
        }
        [Required]
        [Display(Name = "Előadó neve")]
        public string PerformerName { get; private set; }

        [Display(Name = "Dalok")]
        public virtual ICollection<PerformerSong> Songs => _songs.ToList();

        public static Result<SongPerformer> Create(string performerName)
        {
            if(string.IsNullOrWhiteSpace(performerName))
            {
                return Result.Failure<SongPerformer>(new Error("PerformerName.Missing", "Performer name is missing", ErrorType.Validation));
            }
            var performer = new SongPerformer(Guid.NewGuid(), performerName);
            performer.IsActive = true;
            return Result.Success(performer);
        }

        public Result Update(string performerName)
        {
            if(string.IsNullOrWhiteSpace(performerName))
            {
                return Result.Failure(new Error("PerformerName.Missing", "Performer name is missing", ErrorType.Validation));
            }
            PerformerName = performerName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
