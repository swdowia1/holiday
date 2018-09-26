using taskApi.Contract;
using taskApi.DAL;
using taskApi.VM;

namespace taskApi.serwisy
{
    public interface IUserService : IDependency
    {
        ServiceListResponse<string> GetName();
        User GetUser(string email);
        void AddUser(User user);
        ServiceListResponse<UserVM> GetAll();
    }
}