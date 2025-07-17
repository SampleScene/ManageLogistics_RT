using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.ViewModels
{
    public class RegisterUserViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты является обязательным")]
        [Display(Name = "Адрес электронной почты")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Пароль является обязательным")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадаюь")]
        public string ConfirmPassword { get; set; }
    }
}