using ManageLogistics_RT.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.ViewModels
{
    public class CreateStopViewModel
    {
        
        public int Id { get; set; }
        //public Address Address { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Home { get; set; }

        [Required(ErrorMessage = "Поле является обязательным")]
        [Display(Name = "Фото остановки")]
        public IFormFile Image { get; set; }
    }
}
