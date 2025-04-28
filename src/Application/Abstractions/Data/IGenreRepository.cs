using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Data
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="Genre"/> entities.
    /// </summary>
    public interface IGenreRepository : IGenericRepository<Genre>
    {
    }
}
