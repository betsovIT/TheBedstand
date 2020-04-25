namespace TheBedstand.Web.InputModels.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TheBedstand.Web.Common;
    using TheBedstand.Web.ViewModels.Authors;
    using TheBedstand.Web.ViewModels.Genres;

    public class BookInputModel
    {
        [Required]
        [RegularExpression(@"\d{13}|\d{9}[0-9Xx]{1}")]
        [ISBN]
        public string ISBN { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }

        [Range(1, 10000)]
        public int? PageCount { get; set; }

        [Required]
        public string Annotation { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Cover { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int[] GenreIds { get; set; }

        public GenreForSelectListModel[] GenresForSelectList { get; set; }

        public AuthorBasicInfoModel[] AuthorsForSelectList { get; set; }
    }
}
