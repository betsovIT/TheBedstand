namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;

    using TheBedstand.Web.ViewModels.Books;

    public interface IBooksService
    {
        IEnumerable<BookInfoViewModel> All();
    }
}
