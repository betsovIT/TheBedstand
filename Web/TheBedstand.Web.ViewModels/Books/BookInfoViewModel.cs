namespace TheBedstand.Web.ViewModels.Books
{
    using System;
    using System.Collections.Generic;

    using TheBedstand.Data.Models;
    using TheBedstand.Web.ViewModels.Comments;

    public class BookInfoViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedOn { get; set; }

        public string LongAnnotation { get; set; }

        public string ShortAnnotation { get; set; }

        public int? PageCount { get; set; }

        public string CoverUrl { get; set; }

        public Author Author { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<CommentContentViewModel> Comments { get; set; }
    }
}
