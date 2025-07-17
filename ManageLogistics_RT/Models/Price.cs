using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ManageLogistics_RT.Data.Enum;

namespace ManageLogistics_RT.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        [Required]
        [ForeignKey("Terminal")]
        public int TerminalId { get; set; }
        public TypePrice? Type { get; set; }
        public decimal? Fare { get; set; }
        public int? Time { get; set; }
        public int? Number { get; set; }
        public PassReciept? PassReciept { get; set; }
    }
}
