namespace TheBedstand.Web.InputModels.Books
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TheBedstand.Web.Common;
    using TheBedstand.Web.ViewModels.Authors;
    using TheBedstand.Web.ViewModels.Genres;

    public class BookInputModel
    {
        [Required(ErrorMessage = "You must provide an ISBN number.")]
        [RegularExpression(@"\d{13}|\d{9}[0-9Xx]{1}", ErrorMessage = @"The ISBN number must be either 10 or 13 digits long and only the last character may be an ""X""")]
        [ISBN(ErrorMessage = "Not a valid ISBN number.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "The book's Title is required.")]
        [MinLength(1, ErrorMessage = "The book's Title must be at least one character long.")]
        [MaxLength(300, ErrorMessage = "The book's Title can not be more than 300 characters.")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }

        [Range(1, 10000, ErrorMessage = "The total pages must be between 1 and 10000.")]
        public int? PageCount { get; set; }

        [Required(ErrorMessage = "A book annotation must be provided")]
        public string Annotation { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Cover { get; set; }

        [Required(ErrorMessage = "You must choose an author.")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "You must choose at least one genre for the book.")]
        public int[] GenreIds { get; set; }

        public GenreForSelectListModel[] GenresForSelectList { get; set; }

        public AuthorBasicInfoModel[] AuthorsForSelectList { get; set; }
    }
}
