using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.ViewModels.PriceVM
{
    public class PriceListViewModel
    {
        public List<Terminal>? terminals {  get; set; }
        public List<Price>? prices { get; set; }
        public string? errorMessage { get; set; }
    }
}
