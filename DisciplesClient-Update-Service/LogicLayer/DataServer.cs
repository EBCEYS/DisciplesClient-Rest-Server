using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ApiModels.ApiModels;
using Disciples2ClientDataBaseLibrary.DBModels;
using DisciplesClient_Update_Service.LogicLayer.Exceptions;
using DisciplesClient_Update_Service.LogicLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DisciplesClient_Update_Service.LogicLayer
{
    /// <summary>
    /// The data server.
    /// </summary>
    public class DataServer : IDataServer
    {
        private readonly Logger logger;
        private readonly IUsersDBAdapter usersDBAdapter;

        /// <summary>
        /// The ctor for data server.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="usersDBAdapter"></param>
        public DataServer(Logger logger, IUsersDBAdapter usersDBAdapter)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.usersDBAdapter = usersDBAdapter ?? throw new ArgumentNullException(nameof(usersDBAdapter));
        }

        private static string CreateHash(string input)
        {
            byte[] hashed = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hashed);
        }

        /// <summary>
        /// The process login request.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>The generated token if user exists; otherwise throws <see cref="Exception"/>.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> Login(LoginModel loginModel)
        {
            if (loginModel is null)
            {
                throw new ArgumentNullException(nameof(loginModel));
            }

            User user = await usersDBAdapter.LoginUserAsync(loginModel.UserName, CreateHash(loginModel.Password));
            if (user != null)
            {
                logger.Debug("Found user {username}!", user.UserName);
                return GenerateToken(user.UserName, user.Roles);
            }
            throw new UserNotFoundException("User does not exists!");
        }

        private static string GenerateToken(string userName, string[] roles)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Empty user name property on server response!");
            }
            if (roles == null || !roles.Any())
            {
                throw new Exception("Empty role list!");
            }
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, userName),
            };
            foreach (string role in roles)
            {
                claims.Add(new(ClaimTypes.Role, role));
            }
            ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            JwtSecurityToken jwt = new(
                issuer: Program.Issuer,
                audience: Program.Audience,
                claims: claimsIdentity.Claims,
                //expires: DateTime.UtcNow + TimeSpan.FromDays(5),
                signingCredentials:
                new SigningCredentials(new SymmetricSecurityKey(Program.SecretKey), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="model">The create user model.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UserAlreadyExistsException"></exception>
        public async Task CreateUserAsync(CreateUserModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            bool result = await usersDBAdapter.CreateUserAsync(new User()
            {
                UserName = model.UserName,
                Email = model.Email ?? null,
                Password = CreateHash(model.Password),
                Roles = model.Roles
            });
            if (!result) { throw new UserAlreadyExistsException(); }
        }
        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The user if exists; otherwise throws <see cref="UserNotFoundException"/>.</returns>
        /// <exception cref="UserNotFoundException"></exception>
        public async Task<User> GetUserByIdAsync(int id)
        {
            User result = await usersDBAdapter.GetUserByIdAsync(id);
            if (result == null)
            {
                throw new UserNotFoundException();
            }
            return result;
        }
    }
}
