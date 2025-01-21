using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models.Books
{
    public class Publisher : Entity
    {
        private HashSet<Book> _publishedBooks;

        private Publisher(Guid id, string publisherName) : base(id)
        {
            PublisherName = publisherName;
            _publishedBooks = new HashSet<Book>();
        }

        [Required]
        [Display(Name = "Kiadó")]
        public string PublisherName { get; set; }

        public virtual ICollection<Book> PublishedBooks => _publishedBooks.ToList();

        public static Result<Publisher> Create(string publisherName)
        {
            if(string.IsNullOrWhiteSpace(publisherName))
            {
                return Result.Failure<Publisher>(new Error("PublisherName.Required", "Publisher name is required", ErrorType.Validation));
            }
            var publisher = new Publisher(Guid.NewGuid(), publisherName);
            publisher.IsActive = true;
            return Result.Success(publisher);
        }

        public Result Update(string publisherName)
        {
            if (string.IsNullOrWhiteSpace(publisherName))
            {
                return Result.Failure(new Error("PublisherName.Required", "Publisher name is required", ErrorType.Validation));
            }
            PublisherName = publisherName;
            UpdatedAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
