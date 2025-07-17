using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле является обязательным")]
        [Display(Name = "Адрес электронной почты")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Поле является обязательным")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
