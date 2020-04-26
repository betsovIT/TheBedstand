namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Comments;
    using TheBedstand.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<Comment> Create(CommentInputModel input);

        Task<bool> Delete(string id);

        Task<Comment> Edit(CommentIdInputModel input);
    }
}
