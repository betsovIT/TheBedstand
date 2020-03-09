namespace TheBedstand.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TheBedstand.Data.Common.Models;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        public string Name { get; set; }

        public string Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? PseudonymForId { get; set; }

        public Author PseudonymFor { get; set; }

        [Required]
        public string Country { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
