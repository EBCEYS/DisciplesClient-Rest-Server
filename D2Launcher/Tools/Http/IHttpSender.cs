using Disciples2ApiModels.D2ApiModels;

namespace D2Launcher.Tools.Http
{
    public interface IHttpSender
    {
        Task<string> DownloadModFileAsync(string modName, string modDir);
        string GetInfo();
        ModInfo[] GetModsInfo();
        ModInfo[] GetSoftInfo();
    }
}