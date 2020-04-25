namespace TheBedstand.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TheBedstand.Data.Common.Models;
    using TheBedstand.Data.Models.Enums;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [MinLength(2)]
        [MaxLength(20)]
        public string PersonalName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Surname { get; set; }

        public string Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? PseudonymForId { get; set; }

        public Author PseudonymFor { get; set; }

        [Required]
        public Country Country { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
