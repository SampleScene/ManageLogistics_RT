using System.ComponentModel.DataAnnotations;

namespace ManageLogistics_RT.Models
{
    public class Terminal
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        //public string Address { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Home { get; set; }
        public int Capacity { get; set; }
    }
}