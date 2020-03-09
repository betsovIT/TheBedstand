namespace TheBedstand.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TheBedstand.Data.Common.Models;

    public class Book : BaseDeletableModel<string>
    {
        public Book()
        {
            this.BookGenres = new HashSet<BookGenre>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Title { get; set; }

        public DateTime PublishedOn { get; set; }

        [Range(1, 1000)]
        public int? PageCount { get; set; }

        public string CoverUrl { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
