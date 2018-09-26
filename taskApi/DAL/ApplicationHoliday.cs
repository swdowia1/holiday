using System;
using taskApi.Enums;

namespace taskApi.DAL
{
    public class ApplicationHoliday : Entity
    {
        public ApplicationType ApplicationType { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? AproveID { get; set; }
        public virtual User Aprove { get; set; }

    }
}