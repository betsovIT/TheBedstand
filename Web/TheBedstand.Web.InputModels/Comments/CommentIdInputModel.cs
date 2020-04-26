namespace TheBedstand.Web.InputModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentIdInputModel
    {
        [Required]
        public string Id { get; set; }

        public string Content { get; set; }

        public string BookId { get; set; }

        public string UserId { get; set; }
    }
}
