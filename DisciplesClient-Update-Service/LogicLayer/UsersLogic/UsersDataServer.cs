using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ApiModels.ApiModels;
using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic.Exceptions;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic.Interfaces;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserCache;

namespace DisciplesClient_Update_Service.LogicLayer.UsersLogic
{
    /// <summary>
    /// The data server.
    /// </summary>
    public class UsersDataServer : IUsersDataServer
    {
        private readonly Logger logger;
        private readonly IUsersDBAdapter usersDBAdapter;
        private readonly IUsersCacheAdapter usersCache;

        /// <summary>
        /// The ctor for data server.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="usersDBAdapter">The user data base adapter.</param>
        /// <param name="usersCache">The users cache.</param>
        public UsersDataServer(Logger logger, IUsersDBAdapter usersDBAdapter, IUsersCacheAdapter usersCache)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.usersDBAdapter = usersDBAdapter ?? throw new ArgumentNullException(nameof(usersDBAdapter));
            this.usersCache = usersCache ?? throw new ArgumentNullException(nameof(usersCache));
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
                return GenerateToken(user.Id, user.UserName, user.Roles, user.Password);
            }
            throw new UserNotFoundException();
        }

        private static string GenerateToken(int id, string userName, string[] roles, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Empty user name property on server response!");
            }
            if (roles == null || !roles.Any())
            {
                throw new Exception("Empty role list!");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"\"{nameof(password)}\" не может быть пустым или содержать только пробел.", nameof(password));
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Hash, password)
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

        /// <summary>
        /// Deletes the user async.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <exception cref="UserNotFoundException"></exception>
        public async Task DeleteUserById(int id)
        {
            bool res = await usersDBAdapter.DeleteUserAsync(id);
            if (!res)
            {
                throw new UserNotFoundException();
            }
        }
        /// <summary>
        /// Changes user password.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="model">The change password model.</param>
        /// <returns>true if changed; otherwise false.</returns>
        /// <exception cref="UserNotFoundException"></exception>
        public async Task ChangePasswordAsync(int id, ChangePasswordModel model)
        {
            ChangePasswordModel hashedModel = model.CreateModelWithHashedPasswords(CreateHash);
            bool res = await usersDBAdapter.ChangePasswordAsync(id, hashedModel);
            if (!res)
            {
                throw new UserNotFoundException();
            }
        }
        /// <summary>
        /// Changes user email.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="model">The change email model.</param>
        /// <returns>true if changed; otherwise false.</returns>
        /// <exception cref="UserNotFoundException"></exception>
        public async Task ChangeEmailAsync(int id, ChangeEmailModel model)
        {
            bool res = await usersDBAdapter.ChangeEmailAsync(id, model);
            if (!res)
            {
                throw new UserNotFoundException();
            }
        }
        /// <summary>
        /// Changes user UserName.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="model">The change UserName model.</param>
        /// <returns>true if changed; otherwise false.</returns>
        /// <exception cref="UserNotFoundException"></exception>
        public async Task ChangeUserNameAsync(int id, ChangeUserNameModel model)
        {
            bool res = await usersDBAdapter.ChangeUserNameAsync(id, model);
            if (!res)
            {
                throw new UserNotFoundException();
            }
        }
        /// <summary>
        /// Logouts the user. Removes user from cache.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>true if successfuly removed user from cache; otherwise false.</returns>
        public bool Logout(int userId) => usersCache.RemoveUser(userId);
    }
}
