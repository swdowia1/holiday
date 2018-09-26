using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using taskApi.Enums;

namespace taskApi.Core
{
    public class AllowAuthorizedAttribute : AuthorizeAttribute
    {
        public AllowAuthorizedAttribute(params RoleApp[] roles) : base()
        {
            Roles = string.Join(",", roles.Select(k => k.ToString()).ToArray());
        }
    }
}