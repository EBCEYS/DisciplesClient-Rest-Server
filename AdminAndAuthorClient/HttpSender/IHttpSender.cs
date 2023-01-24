using AdminAndAuthorClient.UserForms.ChangeForms;
using Disciples2ApiModels.ApiModels;
using Disciples2ApiModels.D2ApiModels;
using Disciples2ClientDataBaseModels.DBModels;

namespace AdminAndAuthorClient.Http
{
    public interface IHttpSender
    {
        event Action UnAuthorizedError;

        bool ChangeEmail(ChangeEmailModel model);
        bool ChangePassword(ChangePasswordModel model);
        bool ChangeRequest(ChangeTypes type, object data);
        bool ChangeUserName(ChangeUserNameModel model);
        AuthorizedInfo CheckAuthorized();
        bool CreateUser(string userName, string password, string email, params string[] roles);
        void DeleteModFile(string modName, string fileName);
        Task DeleteModFileAsync(string modName, string fileName);
        bool DeleteUserById(int id);
        Task DownloadModFileAsync(string modName, string modFileSavePath, string extractPath);
        string GetInfo();
        string[] GetModFiles(string modName);
        ModInfo[] GetModsInfo();
        Task<ModInfo[]> GetModsInfoAsync();
        ModInfo[] GetSoftInfo();
        User GetUserById(int id);
        bool Logout();
        bool PostInfo(string info);
        string PostLoginRequest(string userName, string password);
        Task<string> PostLoginRequestAsync(string userName, string password);
        void SetToken(string token);
        Task UploadModFileAync(string modName, string version, string file, DateTimeOffset? updateDateTime = null);
        Task UploadSoftFileAync(string softName, string version, string file, DateTimeOffset? updateDateTime = null);
    }
}