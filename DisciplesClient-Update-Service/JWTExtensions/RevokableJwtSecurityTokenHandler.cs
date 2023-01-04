using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DisciplesClient_Update_Service.JWTExtensions
{
    /// <summary>
    /// Get it from https://stackoverflow.com/questions/52476325/how-to-invalidate-tokens-after-password-change.
    /// </summary>
    public class RevokableJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        private readonly IUsersDBAdapter dbAdapter;
        /// <summary>
        /// Initiates the <see cref="RevokableJwtSecurityTokenHandler"/>.
        /// </summary>
        /// <param name="dbAdapter">The users db adapter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RevokableJwtSecurityTokenHandler(IUsersDBAdapter dbAdapter)
        {
            this.dbAdapter = dbAdapter ?? throw new ArgumentNullException(nameof(dbAdapter));
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
            if (!CheckParams(userName, pas, roles))
            {
                throw new SecurityTokenValidationException();
            }
            return claimsPrincipal;
        }

        private bool CheckParams(Claim username, Claim pas, Claim[] roles)
        {
            try
            {
                if (username is null)
                {
                    throw new ArgumentNullException(nameof(username));
                }
                if (pas is null)
                {
                    throw new ArgumentNullException(nameof(pas));
                }
                if (roles is null)
                {
                    throw new ArgumentNullException(nameof(roles));
                }
                List<string> rolesList = new();
                foreach(Claim role in roles)
                {
                    rolesList.Add(role.Value);
                }
                return dbAdapter.CheckUserExists(username.Value, rolesList.ToArray(), pas.Value);
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
