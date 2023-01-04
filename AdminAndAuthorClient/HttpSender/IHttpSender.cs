using Disciples2ApiModels.ApiModels;
using Disciples2ApiModels.D2ApiModels;

namespace AdminAndAuthorClient.Http
{
    public interface IHttpSender
    {
        AuthorizedInfo CheckAuthorized();
        void DeleteModFile(string modName, string fileName);
        Task DeleteModFileAsync(string modName, string fileName);
        Task DownloadModFileAsync(string modName, string modFileSavePath, string extractPath);
        string[] GetModFiles(string modName);
        ModInfo[] GetModsInfo();
        Task<ModInfo[]> GetModsInfoAsync();
        string PostLoginRequest(string userName, string password);
        Task<string> PostLoginRequestAsync(string userName, string password);
        void SetToken(string token);
        Task UploadModFileAync(string modName, string version, string file, DateTimeOffset? updateDateTime = null);
    }
}