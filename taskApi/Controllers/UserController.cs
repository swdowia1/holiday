using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using taskApi.serwisy;

namespace taskApi.Controllers
{
    public class UserController : HolidayAppController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //GetCustomerById
        [HttpGet]
        public HttpResponseMessage getall()
        {
            var response = _userService.GetAll();
            return response.Response(Request);
        }
    }
}
