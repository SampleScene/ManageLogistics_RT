using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageLogistics_RT.Models
{
    public class Way
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Terminal")]
        public int TerminalId { get; set; }
        public string Description { get; set; }
    }
}
