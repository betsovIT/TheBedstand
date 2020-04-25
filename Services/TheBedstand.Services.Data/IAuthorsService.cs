namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Authors;
    using TheBedstand.Web.ViewModels.Authors;

    public interface IAuthorsService
    {
        Task CreateAsync(AuthorInputModel input, ImageUploadResult result);

        IEnumerable<AuthorBasicInfoModel> GetAuthorBasicInfo();

        Author GetById(int id);

        Task PersistEditedAuthorToDb();

        Task Delete(int id);
    }
}
