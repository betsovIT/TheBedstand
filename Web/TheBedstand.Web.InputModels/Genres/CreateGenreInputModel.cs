namespace TheBedstand.Web.InputModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGenreInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
