namespace TheBedstand.Data.Models
{
    using System;

    using TheBedstand.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string BookId { get; set; }

        public Book Book { get; set; }
    }
}
