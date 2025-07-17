using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.ViewModels.UserQuery
{
    public class UserToPriceViewModel
    {
        public List<TripReceipt>? tripReceipts {  get; set; }
        public int? ticketCount { get; set; }
        public int? travelCardCount { get; set; }
        public int? allTrips {  get; set; }
        public string? Query { get; set; }


    }
}
