using SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Audio
{
    /// <summary>
    /// Represents the association between a performer and a song.
    /// </summary>
    public class PerformerSong : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformerSong"/> class.
        /// It is used for EF Core only.
        /// </summary>
        /// <param name="id"></param>
        private PerformerSong(Guid id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformerSong"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the performer-song association.</param>
        /// <param name="performerID">The unique identifier of the performer.</param>
        /// <param name="songID">The unique identifier of the song.</param>
        private PerformerSong(Guid id, Guid performerID, Guid songID) : base(id)
        {
            PerformerID = performerID;
            SongID = songID;
        }

        /// <summary>
        /// Gets the unique identifier of the performer.
        /// </summary>
        [Required]
        public Guid PerformerID { get; private set; }

        /// <summary>
        /// Gets the performer associated with the song.
        /// </summary>
        public SongPerformer Performer { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the song.
        /// </summary>
        [Required]
        public Guid SongID { get; private set; }

        /// <summary>
        /// Gets the song associated with the performer.
        /// </summary>
        public Song Song { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="PerformerSong"/> class.
        /// </summary>
        /// <param name="performerID">The unique identifier of the performer.</param>
        /// <param name="songID">The unique identifier of the song.</param>
        /// <returns>
        /// A <see cref="Result{TValue}"/> containing the created <see cref="PerformerSong"/> instance
        /// or an error if validation fails.
        /// </returns>
        public static Result<PerformerSong> Create(Guid performerID, Guid songID)
        {
            if (performerID == Guid.Empty)
            {
                return Result.Failure<PerformerSong>(new Error("PerformerID.Missing", "Performer ID is missing", ErrorType.Validation));
            }
            if (songID == Guid.Empty)
            {
                return Result.Failure<PerformerSong>(new Error("SongID.Missing", "Song ID is missing", ErrorType.Validation));
            }
            var performerSong = new PerformerSong(Guid.NewGuid(), performerID, songID);
            performerSong.IsActive = true;
            return Result.Success(performerSong);
        }
    }
}
