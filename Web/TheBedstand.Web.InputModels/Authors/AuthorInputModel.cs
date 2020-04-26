namespace TheBedstand.Web.InputModels.Authors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using TheBedstand.Data.Models.Enums;
    using TheBedstand.Web.ViewModels.Authors;

    public class AuthorInputModel
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "The personal name should be at least 2 characters long.")]
        [MaxLength(20, ErrorMessage = "The personal name should be at most 20 characters long.")]
        [Display(Name = "Personal Name")]
        public string PersonalName { get; set; }

        [Required(ErrorMessage = "You must enter a Surname.")]
        [MinLength(2, ErrorMessage = "The personal name should be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "The personal name should be at most 50 characters long.")]
        public string Surname { get; set; }

        public string Biography { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public int? PseudonymForId { get; set; }

        [Required(ErrorMessage = "You must designate the author's Country.")]
        public Country Country { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<AuthorBasicInfoModel> AuthorsForSelectList { get; set; }
    }
}
