namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TheBedstand.Web.InputModels.Books;
    using TheBedstand.Web.ViewModels.Books;

    public interface IBooksService
    {
        IEnumerable<BookInfoViewModel> All();

        Task Create(BookInputModel input, string imageUrl);
    }
}
