using System;
using System.ComponentModel.DataAnnotations;
using taskApi.Enums;

namespace taskApi.DAL
{
    public class Calendar : Entity
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public HolidayType HolidayType { get; set; }
        public DateTime Day { get; set; }
    }
}