namespace TheBedstand.Web.ViewModels.Genres
{
    using System.Collections.Generic;

    using TheBedstand.Data.Models;

    public class AllGenresViewModel
    {
        public IEnumerable<GenreInfoViewModel> Genres { get; set; }
    }
}
