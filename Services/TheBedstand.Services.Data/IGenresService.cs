namespace TheBedstand.Services.Data
{
    using System.Threading.Tasks;

    using TheBedstand.Web.ViewModels.Genres;

    public interface IGenresService
    {
        Task CreateAsync(string name);

        AllGenresViewModel GetAll();
    }
}
