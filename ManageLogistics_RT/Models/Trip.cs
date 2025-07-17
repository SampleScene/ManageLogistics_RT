using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageLogistics_RT.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Driver")]
        public string? DriverId { get; set; }
        [Required]
        [ForeignKey("Route")]
        public int? RouteId { get; set; }
        [Required]
        [ForeignKey("Bus")]
        public int? BusId { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
    }
}
