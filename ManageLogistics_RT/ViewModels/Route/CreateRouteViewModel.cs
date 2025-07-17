using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.ViewModels.Route
{
    public class CreateRouteViewModel
    {
        public int terminalId { get; set; }
        public List<Stop> stops { get; set; }
        public string description {  get; set; }
    }
}
