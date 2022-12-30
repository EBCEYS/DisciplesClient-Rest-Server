using Disciples2ClientDataBaseLibrary.DBModels;

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
    }
}