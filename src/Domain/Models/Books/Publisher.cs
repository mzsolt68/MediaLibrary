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

        public static Publisher Create(string publisherName)
        {
            var publisher = new Publisher(Guid.NewGuid(), publisherName);
            publisher.IsActive = true;
            return publisher;
        }

        public void Update(string publisherName)
        {
            PublisherName = publisherName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
