﻿namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Common;
    using TheBedstand.Common.Helpers;
    using TheBedstand.Data.Models;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Comments;
    using TheBedstand.Web.ViewModels.Comments;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CommentContentViewModel))]
        public async Task<ActionResult<CommentContentViewModel>> CreateComment(CommentInputModel input)
        {
            var comment = await this.commentsService.Create(input);
            var user = this.userManager.FindByIdAsync(input.UserId);

            var result = new CommentContentViewModel
            {
                Id = comment.Id,
                Content = comment.Content,
                Username = user.Result.UserName,
                UserAvatarUrl = await this.cloudinaryService.GetUrlById(user.Result.AvatarId),
                CreatedOn = DateTime.UtcNow,
            };

            return this.Ok(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteComment(CommentIdInputModel input)
        {
            var result = await this.commentsService.Delete(input.Id);

            return this.Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> EditComment(CommentIdInputModel input)
        {
            if (this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value != input.UserId)
            {
                return this.StatusCode(400);
            }

            var result = await this.commentsService.Edit(input);

            if (result == null)
            {
                return this.StatusCode(400);
            }
            else
            {
                return this.Ok(result);
            }
        }
    }
}
