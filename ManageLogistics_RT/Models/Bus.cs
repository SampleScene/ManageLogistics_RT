using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageLogistics_RT.Models
{
    public class Bus
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Terminal")]
        [Required]
        public int TerminalId { get; set; }
        public string Model {  get; set; }
        public int Capacity { get; set; }
        public string? Status { get; set; }
    }
}
