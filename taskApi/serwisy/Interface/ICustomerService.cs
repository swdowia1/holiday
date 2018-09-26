using taskApi.Contract;
using taskApi.VM;

namespace taskApi.serwisy
{
    public interface ICustomerService : IDependency
    {
        ServiceListResponse<CustomerVM> GetCustomers();

        ServiceResponse<int> SaveCustomer(CustomerAddVM add);
        ServiceResponse<CustomerVM> GetCustomerById(int id);
        ServiceBaseResponse DeleteCustomer(int id);
    }
}