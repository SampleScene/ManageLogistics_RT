using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageLogistics_RT.Models
{
    public class TripReceipt
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PassReciept")]
        public int? PassId { get; set; }
        [ForeignKey("Trip")]
        public int TripId { get; set; }
        public DateTime DateOfoperation { get; set; }
        public string  Operation {  get; set; }
    }
}
