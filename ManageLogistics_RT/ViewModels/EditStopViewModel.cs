using ManageLogistics_RT.Helpers;

namespace ManageLogistics_RT.ViewModels
{
    public class EditStopViewModel
    {
        public int Id { get; set; }
        //public Address Address { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Home { get; set; }
        public IFormFile? Image { get; set; }
        public string? URL { get; set; }
    }
}
