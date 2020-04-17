using System;
using System.Collections.Generic;
using System.Text;

namespace TheBedstand.Web.ViewModels.Comments
{
    public class CommentContentViewModel
    {
        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserAvatarId { get; set; }
    }
}
