using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageLogistics_RT.Models
{
    public class Driver
    {
        [Key]
        [ForeignKey("Employee")]
        public string Id { get; set; }
        public string DriverLicenses { get; set; }
    }
}
