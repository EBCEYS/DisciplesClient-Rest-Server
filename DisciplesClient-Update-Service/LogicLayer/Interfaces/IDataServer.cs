using Disciples2ApiModels.ApiModels;
using Disciples2ClientDataBaseLibrary.DBModels;

namespace DisciplesClient_Update_Service.LogicLayer.Interfaces
{
    /// <summary>
    /// The data server interface.
    /// </summary>
    public interface IDataServer
    {
        /// <summary>
        /// Create the user async.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateUserAsync(CreateUserModel model);
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