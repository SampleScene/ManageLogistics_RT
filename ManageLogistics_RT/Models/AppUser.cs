using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.Models
{
    public class AppUser
    {
        [Key]
        [ForeignKey("UserAuthData")]
        public string UserId { get; set; }
        public UserAuthData? UserAuthData { get; set; }
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? Birthday { get; set; }
        public decimal? Balance { get; set; }
    }
}
