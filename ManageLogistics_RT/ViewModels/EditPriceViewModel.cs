using ManageLogistics_RT.Data.Enum;

namespace ManageLogistics_RT.ViewModels
{
    public class EditPriceViewModel
    { public int Id { get; set; }
        public string? Title { get; set; }
        public int TerminalId { get; set; }
        public TypePrice? Type { get; set; }
        public decimal? Fare { get; set; }
        public int? Time { get; set; }
        public int? Number { get; set; }
    }
}
