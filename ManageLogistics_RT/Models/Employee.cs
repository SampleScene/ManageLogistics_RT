using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageLogistics_RT.Models
{
    public class Employee
    {
        [Key]
        [ForeignKey("UserAuthData")]
        public string EmployeeId { get; set; }

        [Required]
        [ForeignKey("Terminal")]
        public int TerminalId { get; set; }
        public Terminal? Terminal { get; set; }
        public UserAuthData? UserAuthData { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SerisesAndNumber { get; set; }
        public string? IssuedBy { get; set; }
        //public string? Pasport { get; set; }
        public string? Image { get; set; }
    }
}
