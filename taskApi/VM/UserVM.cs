using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskApi.DAL;

namespace taskApi.VM
{
    public class UserVM : ViewModelBase
    {
        public string Token { get; set; }
        public bool IsLogin { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}