using ManageLogistics_RT.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageLogistics_RT.ViewModels.TripViewModel
{
    public class CreateTripViewModel
    {
        public List<SelectListItem>? Drivers {  get; set; }
        public string? SelectedDriver { get; set; }
        public List<SelectListItem>? Routes { get; set; }
        public int? SelectedRoute { get; set; }
        public List<SelectListItem>? Buses { get; set; }
        public int? SelectedBus { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string? MessageError { get; set; }
    }
}
