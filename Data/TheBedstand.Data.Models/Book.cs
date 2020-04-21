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
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Title { get; set; }

        public DateTime PublishedOn { get; set; }

        [Range(1, 10000)]
        public int? PageCount { get; set; }

        [Required]
        public string Annotation { get; set; }

        public string CoverUrl { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
