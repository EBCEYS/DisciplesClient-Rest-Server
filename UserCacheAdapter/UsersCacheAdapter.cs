using Disciples2ClientDataBaseModels.DBModels;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.LayoutRenderers;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace UserCache
{
    /// <summary>
    /// The users cache adapter. SINGLETON ONLY!
    /// </summary>
    public class UsersCacheAdapter : IUsersCacheAdapter
    {
        private readonly Logger logger;

        private ConcurrentDictionary<int, User> usersCash;
        public UsersCacheAdapter(Logger logger, ConcurrentDictionary<int, User> usersCash)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.usersCash = usersCash ?? throw new ArgumentNullException(nameof(usersCash));
        }

        public void AddOrReplaceUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            usersCash[user.Id] = user;
        }

        public bool RemoveUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            bool result = usersCash.TryRemove(user.Id, out _);
            logger.Debug("Remove user from cache result is: {result}", result);
            return result;
        }

        public bool RemoveUser(int userId)
        {
            bool result = usersCash.TryRemove(userId, out _);
            logger.Debug("Remove user from cache result is: {result}", result);
            return result;
        }

        public bool CheckUserParameters(Claim id, Claim username, Claim pas, Claim[] roles)
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
                foreach (Claim role in roles)
                {
                    rolesList.Add(role.Value);
                }
                if (usersCash.TryGetValue(userId, out User u))
                {
                    return CheckParams(u, pas.Value, username.Value, rolesList.ToArray());
                }
                return false;

            }
            catch (Exception ex)
            {
                logger.Error("Error on validating token! {ex}", ex.Message);
                return false;
            }
        }
        private static bool CheckParams(User u, string password, string username, string[] roles)
        {
            return u.IsActive && u.Password == password && u.UserName == username && u.Roles.SequenceEqual(roles);
        }
    }
}