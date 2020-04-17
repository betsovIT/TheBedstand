namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Data.Models;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Comments;
    using TheBedstand.Web.ViewModels.Comments;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommentContentViewModel>> GetComments(string bookId)
        {
            var result = this.commentsService.GetBookComments(bookId).ToList();

            return result;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CommentContentViewModel))]
        public async Task<ActionResult<CommentContentViewModel>> Create(CommentInputModel input)
        {
            var comment = await this.commentsService.Create(input);
            var user = this.userManager.FindByIdAsync(input.UserId);

            var result = new CommentContentViewModel
            {
                Content = comment.Content,
                Username = user.Result.UserName,
                UserAvatarId = user.Result.AvatarId,
                CreatedOn = comment.CreatedOn,
            };

            return this.Ok(result);
        }
    }
}
