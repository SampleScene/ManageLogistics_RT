using ManageLogistics_RT.Data;
using ManageLogistics_RT.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.ViewModels.Employee
{
    public class CreateEmployeeViewModel
    {
     

        [Required(ErrorMessage = "Адрес электронной почты является обязательным")]
        [Display(Name = "Адрес электронной почты")]
        public string EmailAddress { get; set; }
        public int TerminalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SerialAndNumber { get; set; }
        public string IssuedBy { get; set; }
        public IFormFile? Image { get; set; }
        public EmployeeRoles Roles { get; set; }
        [Required(ErrorMessage = "Пароль является обязательным")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадаюь")]
        public string ConfirmPassword { get; set; }
     
    }
}