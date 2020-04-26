namespace TheBedstand.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class BooksIndexViewModel
    {
        public IEnumerable<BookInfoViewModel> Items { get; set; }

        public Pager Pager { get; set; }
    }
}
