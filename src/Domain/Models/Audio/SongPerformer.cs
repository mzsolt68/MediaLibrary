using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SharedKernel;

namespace Domain.Models.Audio
{
    /// <summary>
    /// Represents a performer of songs in the domain.
    /// </summary>
    public class SongPerformer : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongPerformer"/> class.
        /// It is used for EF Core only
        /// </summary>
        /// <param name="id"></param>
        private SongPerformer(Guid id) : base(id) { }

        private HashSet<PerformerSong> _songs;

        /// <summary>
        /// Initializes a new instance of the <see cref="SongPerformer"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the performer.</param>
        /// <param name="performerName">The name of the performer.</param>
        private SongPerformer(Guid id, string performerName) : base(id)
        {
            PerformerName = performerName;
            _songs = new HashSet<PerformerSong>();
        }

        /// <summary>
        /// Gets the name of the performer.
        /// </summary>
        [Required]
        [Display(Name = "Előadó neve")]
        public string PerformerName { get; private set; }

        /// <summary>
        /// Gets the collection of songs associated with the performer.
        /// </summary>
        [Display(Name = "Dalok")]
        public virtual ICollection<PerformerSong> Songs => _songs.ToList();

        /// <summary>
        /// Creates a new instance of the <see cref="SongPerformer"/> class.
        /// </summary>
        /// <param name="performerName">The name of the performer.</param>
        /// <returns>A <see cref="Result{TValue}"/> containing the created performer or an error.</returns>
        public static Result<SongPerformer> Create(string performerName)
        {
            if (string.IsNullOrWhiteSpace(performerName))
            {
                return Result.Failure<SongPerformer>(new Error("PerformerName.Missing", "Performer name is missing", ErrorType.Validation));
            }
            var performer = new SongPerformer(Guid.NewGuid(), performerName);
            performer.IsActive = true;
            return Result.Success(performer);
        }

        /// <summary>
        /// Updates the name of the performer.
        /// </summary>
        /// <param name="performerName">The new name of the performer.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        public Result Update(string performerName)
        {
            if (string.IsNullOrWhiteSpace(performerName))
            {
                return Result.Failure(new Error("PerformerName.Missing", "Performer name is missing", ErrorType.Validation));
            }
            PerformerName = performerName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
