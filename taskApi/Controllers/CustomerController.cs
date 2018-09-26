using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using taskApi.Core;
using taskApi.Enums;
using taskApi.serwisy;
using taskApi.VM;

namespace taskApi.Controllers
{

    // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : HolidayAppController

    {
        private readonly ICustomerService _service;


        public CustomerController(ICustomerService service)
        {
            _service = service;
        }




        [HttpGet]
        [AllowAuthorized(RoleApp.User, RoleApp.Admin)]
        [Route("api/Customer/GetCustomers")]
        public HttpResponseMessage GetCustomers()
        {

            var response = _service.GetCustomers();
            return response.Response(Request);
        }
        //GetCustomerById
        [HttpGet]
        [Route("api/Customer/GetCustomerById/{id}")]
        public HttpResponseMessage GetCustomerById(int id)
        {
            var response = _service.GetCustomerById(id);
            return response.Response(Request);
        }
        [HttpPost]
        [Route("api/Customer/SaveCustomer")]
        public HttpResponseMessage SaveCustomer(CustomerAddVM add)
        {
            var response = _service.SaveCustomer(add);
            return response.Response(Request);
        }
        //DeleteCustomer
        [HttpDelete]
        [Route("api/Customer/DeleteCustomer/{id}")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            var response = _service.DeleteCustomer(id);
            return response.Response(Request);
        }
    }
}