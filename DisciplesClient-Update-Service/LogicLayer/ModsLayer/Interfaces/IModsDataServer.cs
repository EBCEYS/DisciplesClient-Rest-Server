using Disciples2ApiModels.D2ApiModels;

namespace DisciplesClient_Update_Service.LogicLayer.ModsLayer.Interfaces
{
    /// <summary>
    /// The mods data server.
    /// </summary>
    public interface IModsDataServer
    {
        /// <summary>
        /// Gets mod file.
        /// </summary>
        /// <param name="modName"></param>
        /// <returns></returns>
        Task<Stream> GetModFile(string modName);
        /// <summary>
        /// Gets the mod files array.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <returns></returns>
        Task<string[]> GetModFilesAsync(string modName);

        /// <summary>
        /// Gets the mods names async.
        /// </summary>
        /// <returns></returns>
        Task<ModInfo[]> GetModsNamesAsync(bool isSoftware = false);
        /// <summary>
        /// Removes the mod.
        /// </summary>
        /// <param name="modName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<bool> RemoveModAsync(string modName, string fileName);

        /// <summary>
        /// Uploads the mod.
        /// </summary>
        /// <param name="modName"></param>
        /// <param name="version"></param>
        /// <param name="authorName"></param>
        /// <param name="mod"></param>
        /// <param name="updateDateTime"></param>
        /// <param name="isSoftware"></param>
        /// <returns></returns>
        Task<bool> UploadModAsync(string modName, string version, string authorName, IFormFile mod, DateTimeOffset? updateDateTime = null, bool isSoftware = false);
    }
}