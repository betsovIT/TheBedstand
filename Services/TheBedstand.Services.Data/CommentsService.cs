namespace TheBedstand.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Comments;
    using TheBedstand.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<Comment> Create(CommentInputModel input)
        {
            var comment = new Comment
            {
                BookId = input.BookId,
                UserId = input.UserId,
                Content = input.Content,
                CreatedOn = DateTime.UtcNow,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();

            return comment;
        }

        public IEnumerable<CommentContentViewModel> GetBookComments(string bookId)
        {
            var result = this.commentRepository.All()
                .Select(x => new CommentContentViewModel
                {
                    Content = x.Content,
                    Username = x.User.UserName,
                    UserAvatarId = x.User.AvatarId,
                    CreatedOn = x.CreatedOn,
                }).AsEnumerable();

            return result;
        }
    }
}
