using ManageLogistics_RT.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.ViewModels.UserDashboard
{
    public class PoezdkiViewModel
    {
       
        public int Id { get; set; }

        [Required]
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        
        [Required]
        [ForeignKey("Price")]
        public int PriceId { get; set; }
       
        public DateTime? TimeStump { get; set; }

        public int? Poezdki {  get; set; }
    }
}
