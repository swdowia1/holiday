using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using taskApi.Contract;
using taskApi.Core;
using taskApi.Enums;
using taskApi.serwisy;
using taskApi.VM;

namespace taskApi.Controllers
{

    public class MyHolidayController : HolidayAppController
    {
        private readonly IHolidayService _service;
        public MyHolidayController(IHolidayService service)
        {
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage GetHoliday()
        {
            var response = _service.GetGetHoliday();

            return response.Response(Request);
        }
        [HttpGet]
        [Route("api/MyHoliday/test")]
        public HttpResponseMessage GetHoliday2()
        {
            var response = _service.GetGetHoliday2();

            return response.Response(Request);
        }


        [HttpPost]
        [Route("api/MyHoliday/AddCalendar")]
        public HttpResponseMessage AddCalendar(AddHolidayVM add)
        {
            var response = _service.AddUserHoliday(add);

            return response.Response(Request);
        }
    }
}
