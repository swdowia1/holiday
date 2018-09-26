using JWT;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using taskApi.Contract;
using taskApi.Core;
using taskApi.DAL;
using taskApi.serwisy;
using taskApi.VM;

namespace taskApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private readonly IUserService _service;


        public AccountController(IUserService service)
        {
            _service = service;
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
        /// <summary>
        /// Create a Jwt with user information
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dbUser"></param>
        /// <returns></returns>
        private static string CreateToken(User user)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiry = Math.Round((DateTime.UtcNow.AddHours(2) - unixEpoch).TotalSeconds);
            var issuedAt = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);
            var notBefore = Math.Round((DateTime.UtcNow.AddMonths(6) - unixEpoch).TotalSeconds);


            var payload = new Dictionary<string, object>
            {
                {"email", user.Email},
                {"userId", user.Id},
                {"role", user.RoleApp.ToString()  },
                {"sub", user.Id},
                {"nbf", notBefore},
                {"iat", issuedAt},
                {"exp", expiry}
            };

            //var secret = ConfigurationManager.AppSettings.Get("jwtKey");
            const string apikey = "secretKey";

            var token = JsonWebToken.Encode(payload, apikey, JwtHashAlgorithm.RS256);


            return token;
        }
        [AllowAnonymous]
        [Route("api/rejestracja")]
        [HttpPost]
        public HttpResponseMessage Register(RegisterViewModel model)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = _service.GetUser(model.Email);
                    if (existingUser != null)
                    {
                        result.isError = true;
                        return result.Response(Request);
                    }

                    //Create user and save to database
                    var user = CreateUser(model);


                }
                else
                {
                    result.isError = true;

                }
            }
            catch (Exception ex)
            {

                result.isError = true;
                result.ErrorMessage = ex.Message;

            }


            return result.Response(Request);
        }

        /// <summary>
        /// Create a new user and saves it to the database
        /// </summary>
        /// <param name="registerDetails"></param>
        /// <returns></returns>
        private User CreateUser(RegisterViewModel registerDetails)
        {
            var passwordSalt = CreateSalt();
            var user = new User
            {
                Salt = passwordSalt,
                Email = registerDetails.Email,
                PasswordHash = EncryptPassword(registerDetails.Password, passwordSalt),
                RoleApp = registerDetails.RoleApp
            };

            _service.AddUser(user);

            return user;
        }

        [AllowAnonymous]
        [Route("api/login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginViewModel model)
        {


            ServiceResponse<UserVM> result = new ServiceResponse<UserVM>();
            if (ModelState.IsValid)
            {
                var existingUser = _service.GetUser(model.Email);

                if (existingUser == null)
                {
                    result.isError = true;


                }
                else
                {
                    var loginSuccess =
                        string.Equals(EncryptPassword(model.Password, existingUser.Salt),
                            existingUser.PasswordHash);

                    if (loginSuccess)
                    {

                        var token = CreateToken(existingUser);
                        result.Result = new UserVM() { IsLogin = true, Id = existingUser.Id };

                        result.Result.Token = token;


                    }
                    else
                    {
                        result.isError = true;
                    }

                }
            }
            else
            {
                result.isError = true;
            }
            return result.Response(Request);
        }
    }
}
