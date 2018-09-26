using System.Collections.Generic;
using taskApi.Enums;

namespace taskApi.DAL
{
    public class User : Entity
    {


        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public RoleApp RoleApp { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
    }
}