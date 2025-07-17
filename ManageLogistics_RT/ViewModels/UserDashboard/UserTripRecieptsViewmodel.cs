using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.ViewModels.UserDashboard
{
    public class UserTripRecieptsViewmodel
    {
       
        public int Id { get; set; }
        public string? Pass { get; set; }
        public int TripId { get; set; }
        public DateTime DateOfoperation { get; set; }
        public string Operation { get; set; }
    }
}
