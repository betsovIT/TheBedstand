namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TheBedstand.Data.Models;
    using TheBedstand.Web.ViewModels.Genres;

    public interface IGenresService
    {
        Task CreateAsync(string name, string description, string imageUrl);

        AllGenresViewModel GetAll();

        IEnumerable<GenreForSelectListModel> GetGenresForSelectList();

        Genre GetbyId(int id);

        Task PersistChanges();

        Task<bool> Delete(int id);
    }
}
