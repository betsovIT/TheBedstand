namespace TheBedstand.Web.InputModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateGenreInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A genre name is required")]
        [MinLength(3, ErrorMessage = "The genre's name must be at least 3 characters.")]
        [MaxLength(20, ErrorMessage = "The genre's name must be 20 characters at most.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="A description for the genre must be provided.")]
        [MinLength(3, ErrorMessage = "The genre's description must be at least 3 characters.")]
        [MaxLength(300, ErrorMessage = "The genre's desciption must be 300 characters at most.")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
    }
}
