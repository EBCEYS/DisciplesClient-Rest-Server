using Disciples2ApiModels.ApiModels;
using Disciples2ClientDataBaseModels.DBModels;

namespace DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface
{
    /// <summary>
    /// The users db adapter.
    /// </summary>
    public interface IUsersDBAdapter
    {
        /// <summary>
        /// Gets user by id if exists.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The user if exists; otherwise null.</returns>
        Task<User> GetUserByIdAsync(int id);
        /// <summary>
        /// Logins the user if exists.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>The user if exists; otherwise null.</returns>
        Task<User> LoginUserAsync(string username, string password);
        /// <summary>
        /// Creates the user if not exists.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>true if created; otherwise false.</returns>
        Task<bool> CreateUserAsync(User user);
        /// <summary>
        /// Deletes the user by id if exists (this method is not actualy deletes the user. its just marks the user to be deleted).
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>true if deleted; otherwise false.</returns>
        Task<bool> DeleteUserAsync(int id);
        /// <summary>
        /// Gets the user by name async.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <returns></returns>
        Task<User> GetUserByUserNameAsync(string name);
        /// <summary>
        /// Gets the author's mods.
        /// </summary>
        /// <param name="id">The author id.</param>
        /// <returns></returns>
        Task<Mod[]> GetAuthorsModsAsync(int id);
        /// <summary>
        /// Gets the author's mods.
        /// </summary>
        /// <param name="name">The author's name.</param>
        /// <returns></returns>
        Task<Mod[]> GetAuthorsModsAsync(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(int id, ChangePasswordModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> ChangeEmailAsync(int id, ChangeEmailModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> ChangeUserNameAsync(int id, ChangeUserNameModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roles"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CheckUserExistsAsync(string username, string[] roles, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roles"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckUserExists(string username, string[] roles, string password);
    }
}