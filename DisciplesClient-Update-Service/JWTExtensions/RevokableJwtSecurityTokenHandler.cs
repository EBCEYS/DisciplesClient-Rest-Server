using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserCache;

namespace DisciplesClient_Update_Service.JWTExtensions
{
    /// <summary>
    /// Get it from https://stackoverflow.com/questions/52476325/how-to-invalidate-tokens-after-password-change.
    /// </summary>
    public class RevokableJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        private readonly IUsersCacheAdapter cacheAdapter;

        /// <summary>
        /// Initiates the <see cref="RevokableJwtSecurityTokenHandler"/>.
        /// </summary>
        /// <param name="cacheAdapter">The users cache adapter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RevokableJwtSecurityTokenHandler(IUsersCacheAdapter cacheAdapter)
        {
            this.cacheAdapter = cacheAdapter ?? throw new ArgumentNullException(nameof(cacheAdapter));
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
            if (!cacheAdapter.CheckUserParameters(id, userName, pas, roles))
            {
                throw new SecurityTokenValidationException();
            }
            return claimsPrincipal;
        }
    }
}
