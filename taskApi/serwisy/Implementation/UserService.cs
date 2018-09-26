using System;
using System.Linq;
using taskApi.Contract;
using taskApi.DAL;
using taskApi.VM;

namespace taskApi.serwisy
{
    public class UserService : IUserService, IDisposable
    {
        private TaskApiContext _dbContext = new TaskApiContext();
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public User GetUser(string email)
        {
            return _dbContext.Users.FirstOrDefault(k => k.Email == email);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Pobranie nazwisk
        /// </summary>
        /// <returns></returns>
        public ServiceListResponse<string> GetName()
        {
            ServiceListResponse<string> result = new ServiceListResponse<string>();

            return result;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public ServiceListResponse<UserVM> GetAll()
        {
            ServiceListResponse<UserVM> result = new ServiceListResponse<UserVM>();

            result.Result = _dbContext.Users.Select(k => new UserVM()
            {
                Id = k.Id,
                Email = k.Email,
                Role = k.RoleApp.ToString()
            });
            return result;
        }
    }
}