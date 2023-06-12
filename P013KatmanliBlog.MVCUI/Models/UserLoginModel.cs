using System.ComponentModel.DataAnnotations;

namespace P013KatmanliBlog.MVCUI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "{0} Boş geçilemez!"), MaxLength(70), Display(Name = "Email"), EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} Boş geçilemez!"), MaxLength(70), Display(Name = "Şifre"), DataType(DataType.Password)]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public string? ReturnUrl { get; set; }
    }
}

