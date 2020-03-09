namespace TheBedstand.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TheBedstand.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.BooksGenre = new HashSet<BookGenre>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<BookGenre> BooksGenre { get; set; }
    }
}
