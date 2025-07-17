using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.ViewModels
{
    public class StopViewModel
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Home { get; set; }
        public string Image { get; set; }
    }
}
