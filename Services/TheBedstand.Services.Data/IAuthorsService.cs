namespace TheBedstand.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Authors;
    using TheBedstand.Web.ViewModels.Authors;

    public interface IAuthorsService
    {
        Task CreateAsync(AuthorInputModel input, string imageUrl);

        IEnumerable<Author> GetAll();
    }
}
