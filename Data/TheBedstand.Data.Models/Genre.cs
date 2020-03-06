using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheBedstand.Data.Common.Models;

namespace TheBedstand.Data.Models
{
    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.BooksGenre = new HashSet<BookGenre>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<BookGenre> BooksGenre { get; set; }
    }
}
