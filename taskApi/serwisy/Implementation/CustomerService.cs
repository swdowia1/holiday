using AutoMapper;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using taskApi.Contract;
using taskApi.DAL;
using taskApi.VM;

namespace taskApi.serwisy
{
    public class CustomerService : ICustomerService
    {
        private TaskApiContext _dbContext = new TaskApiContext();
        private readonly IFunctionService _function;
        private bool disposed = false;
        public CustomerService(IFunctionService function)
        {
            _function = function;
        }
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ServiceListResponse<CustomerVM> GetCustomers()
        {
            RegisterViewModel registerDetails = new RegisterViewModel();
            UserVM g = _function.CurrentUser();
            registerDetails.Email = "sw@wer.pl";
            registerDetails.Password = "Piaseczno!@#";
            var d = _dbContext.Users.ToList();
            ServiceListResponse<CustomerVM> response = new ServiceListResponse<CustomerVM>();
            response.Result = _dbContext.Customers.ToList().Select(k => Mapper.Map<Customer, CustomerVM>(k));
            return response;
        }
        private User CreateUser(RegisterViewModel registerDetails)
        {
            var passwordSalt = CreateSalt();
            var user = new User
            {
                Salt = passwordSalt,
                Email = registerDetails.Email,
                PasswordHash = EncryptPassword(registerDetails.Password, passwordSalt),
                RoleApp = Enums.RoleApp.User
            };

            //user.Roles.Add(new UserRole
            //{
            //    User = user,
            //    Role = adminRole
            //});

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        /// <summary>
        ///     Encrypts a password using the given salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                var saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }

        public static string CreateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        public ServiceResponse<int> SaveCustomer(CustomerAddVM customer)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            if (customer.Id > 0)
            {
                var dbCustomer = _dbContext.Customers.FirstOrDefault(x => x.Id == customer.Id);
                dbCustomer.Name = customer.Name;
                dbCustomer.Address = customer.Address;
                dbCustomer.Phone = customer.Phone;
                _dbContext.SaveChanges();
            }
            else
            {
                Customer aa = Mapper.Map<CustomerAddVM, Customer>(customer);
                _dbContext.Customers.Add(aa);
                _dbContext.SaveChanges();
            }
            return result;
        }

        public ServiceResponse<CustomerVM> GetCustomerById(int id)
        {
            ServiceResponse<CustomerVM> result = new ServiceResponse<CustomerVM>();
            Customer c = _dbContext.Customers.Find(id);
            result.Result = Mapper.Map<Customer, CustomerVM>(c);
            return result;
        }

        public ServiceBaseResponse DeleteCustomer(int id)
        {
            ServiceBaseResponse result = new ServiceBaseResponse();
            try
            {
                var customer = new Customer { Id = id };
                _dbContext.Customers.Attach(customer);
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                result.isError = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}