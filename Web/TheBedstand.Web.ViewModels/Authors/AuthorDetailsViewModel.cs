namespace TheBedstand.Web.ViewModels.Authors
{
    using System;
    using System.Collections.Generic;

    using TheBedstand.Data.Models.Enums;
    using TheBedstand.Web.ViewModels.Books;

    public class AuthorDetailsViewModel
    {
        public string PersonalName { get; set; }

        public string Surname { get; set; }

        public string Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PseudonymForAuthor { get; set; }

        public string Country { get; set; }

        public string ImageUrl { get; set; }

        public string[] Genres { get; set; }

        public ICollection<BookAuthorPageViewModel> Books { get; set; }
    }
}
