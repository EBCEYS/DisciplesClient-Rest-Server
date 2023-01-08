using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ClientDataBaseModels.DBModels;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DisciplesClient_Update_Service.JWTExtensions
{
    /// <summary>
    /// Get it from https://stackoverflow.com/questions/52476325/how-to-invalidate-tokens-after-password-change.
    /// </summary>
    public class RevokableJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        private readonly ConcurrentDictionary<int, User> users;

        /// <summary>
        /// Initiates the <see cref="RevokableJwtSecurityTokenHandler"/>.
        /// </summary>
        /// <param name="users">Cashed users.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RevokableJwtSecurityTokenHandler(ConcurrentDictionary<int, User> users)
        {
            this.users = users ?? throw new ArgumentNullException(nameof(users));
        }
        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="validationParameters">The validation parameters.</param>
        /// <param name="validatedToken">The validated token.</param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenValidationException"></exception>
        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            ClaimsPrincipal claimsPrincipal = base.ValidateToken(token, validationParameters, out validatedToken);
            Claim pas = claimsPrincipal.FindFirst(ClaimTypes.Hash);
            Claim userName = claimsPrincipal.FindFirst(ClaimTypes.Name);
            Claim[] roles = claimsPrincipal.FindAll(ClaimTypes.Role).ToArray();
            Claim id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            if (!CheckParams(id, userName, pas, roles))
            {
                throw new SecurityTokenValidationException();
            }
            return claimsPrincipal;
        }

        private bool CheckParams(Claim id, Claim username, Claim pas, Claim[] roles)
        {
            try
            {
                if (id is null)
                {
                    throw new SecurityTokenValidationException(nameof(id));
                }
                if (username is null)
                {
                    throw new SecurityTokenValidationException(nameof(username));
                }
                if (pas is null)
                {
                    throw new SecurityTokenValidationException(nameof(pas));
                }
                if (roles is null)
                {
                    throw new SecurityTokenValidationException(nameof(roles));
                }
                if (!int.TryParse(id.Value, out int userId))
                {
                    throw new SecurityTokenValidationException("Id is not correct!");
                }
                List<string> rolesList = new();
                foreach(Claim role in roles)
                {
                    rolesList.Add(role.Value);
                }
                if (users.TryGetValue(userId, out User u))
                {
                    return CheckUserParams(u, pas.Value, username.Value, rolesList.ToArray());
                }
                return false;

            }
            catch(Exception)
            {
                return false;
            }
        }

        private static bool CheckUserParams(User u, string password, string username, string[] roles)
        {
            return u.IsActive && u.Password == password && u.UserName == username && u.Roles.SequenceEqual(roles);
        }
    }
}
