namespace TheBedstand.Web.InputModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string BookId { get; set; }
    }
}
