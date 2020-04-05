namespace TheBedstand.Web.InputModels.Authors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using TheBedstand.Web.ViewModels.Authors;

    public class AuthorInputModel
    {
        [MinLength(2)]
        [MaxLength(20)]
        public string PersonalName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Surname { get; set; }

        public string Biography { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public int? PseudonymForId { get; set; }

        [Required]
        public int Country { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<AuthorBasicInfoModel> AuthorsForSelectList { get; set; }
    }
}
