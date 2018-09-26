using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taskApi.VM
{
    public class MyHolidayVM
    {
        public int Id { get; set; }
        public string MonthName { get; set; }
        public List<DayVM> Days { get; set; }
        public MyHolidayVM()
        {
            Days = new List<DayVM>();
        }
    }
}