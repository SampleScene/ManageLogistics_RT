using ManageLogistics_RT.Models;

namespace ManageLogistics_RT.ViewModels.UserDashboard
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }
        public IFormFile? Image { get; set; }
        public string? URL { get; set; }
        public UserAuthData? UserAuthData { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? Birthday { get; set; }
        public decimal? Balance { get; set; }
    }
}
