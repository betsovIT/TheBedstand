namespace TheBedstand.Web.ViewModels.Comments
{
    using System;

    public class CommentContentViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserAvatarUrl { get; set; }

        public string UserId { get; set; }
    }
}
