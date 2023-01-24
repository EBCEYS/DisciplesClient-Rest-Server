using Disciples2ApiModels.D2ApiModels;

namespace D2Launcher.Tools.Http
{
    public interface IHttpSender
    {
        Task<string> DownloadModFileAsync(string modName, string modDir);
        ModInfo[] GetModsInfo();
        ModInfo[] GetSoftInfo();
    }
}