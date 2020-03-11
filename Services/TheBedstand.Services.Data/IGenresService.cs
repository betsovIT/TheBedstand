namespace TheBedstand.Services.Data
{
    using System.Threading.Tasks;
    using TheBedstand.Web.InputModels.Genres;
    using TheBedstand.Web.ViewModels.Genres;

    public interface IGenresService
    {
        Task CreateAsync(CreateGenreInputModel model);

        AllGenresViewModel GetAll();
    }
}
