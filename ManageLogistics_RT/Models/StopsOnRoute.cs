using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageLogistics_RT.Models
{
    public class StopsOnRoute
    {
        [Key]
        [ForeignKey("Route")]
        public int RouteId {  get; set; }
        [Key]
        [ForeignKey("Stop")]
        public int StopId { get; set; }
        public int Order { get; set; }
        public int TransferTime { get; set; }
    }
}
