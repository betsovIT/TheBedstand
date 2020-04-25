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

        public async Task<bool> Delete(string id)
        {
            var comment = this.commentRepository.All().FirstOrDefault(x => x.Id == id);

            this.commentRepository.Delete(comment);
            var result = await this.commentRepository.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
