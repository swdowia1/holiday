using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taskApi.VM
{
    public class UserHoliday : ViewModelBase
    {
        public string Email { get; set; }
        public IEnumerable<string> Daty { get; set; }
    }
}