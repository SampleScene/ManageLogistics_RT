using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.Models
{
    public class PassReciept
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        [Required]
        [ForeignKey("Price")]
        public int PriceId { get; set; }
        public Price? Price { get; set; }
        public DateTime? TimeStump { get; set; }
    }
}
