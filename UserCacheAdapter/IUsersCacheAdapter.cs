using Disciples2ClientDataBaseModels.DBModels;
using System.Security.Claims;

namespace UserCache
{
    public interface IUsersCacheAdapter
    {
        void AddOrReplaceUser(User user);
        bool CheckUserParameters(Claim id, Claim username, Claim pas, Claim[] roles);
        bool RemoveUser(User user);
        bool RemoveUser(int userId);
    }
}