using taskApi.Enums;

namespace taskApi.VM
{
    public class DayVM
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public DayType DayType { get; set; }
        public string Date { get; set; }
    }
}