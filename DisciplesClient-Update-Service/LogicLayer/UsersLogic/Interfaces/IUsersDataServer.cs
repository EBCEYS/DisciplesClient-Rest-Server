using Disciples2ApiModels.ApiModels;
using Disciples2ClientDataBaseModels.DBModels;

namespace DisciplesClient_Update_Service.LogicLayer.UsersLogic.Interfaces
{
    /// <summary>
    /// The data server interface.
    /// </summary>
    public interface IUsersDataServer
    {
        Task ChangeEmailAsync(int id, ChangeEmailModel model);
        Task ChangePasswordAsync(int id, ChangePasswordModel model);
        Task ChangeUserNameAsync(int id, ChangeUserNameModel model);

        /// <summary>
        /// Create the user async.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateUserAsync(CreateUserModel model);
        /// <summary>
        /// Deletes the user async.
        /// </summary>
        /// <param name="id">The user id.</param>
        Task DeleteUserById(int id);

        /// <summary>
        /// Gets user by id async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Process login request.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>The generated token.</returns>
        Task<string> Login(LoginModel loginModel);
    }
}