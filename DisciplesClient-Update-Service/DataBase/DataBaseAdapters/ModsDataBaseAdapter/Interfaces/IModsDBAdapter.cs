using Disciples2ClientDataBaseModels.DBModels;

namespace DisciplesClient_Update_Service.DataBase.DataBaseAdapters.ModsDataBaseAdapter.Interfaces
{
    /// <summary>
    /// The mods data base adapter.
    /// </summary>
    public interface IModsDBAdapter
    {
        /// <summary>
        /// Creates the mod async.
        /// </summary>
        /// <param name="mod">The mod.</param>
        /// <returns></returns>
        Task<bool> CreateModAsync(Mod mod);
        /// <summary>
        /// Gets mod by author.
        /// </summary>
        /// <param name="modName"></param>
        /// <param name="authorName"></param>
        /// <param name="isSoftware"></param>
        /// <returns></returns>
        Task<Mod> GetModByAuthorAsync(string modName, string authorName, bool isSoftware = false);
        /// <summary>
        /// Gets mod by author.
        /// </summary>
        /// <param name="modName"></param>
        /// <param name="authorId"></param>
        /// <param name="isSoftware"></param>
        /// <returns></returns>
        Task<Mod> GetModByAuthorAsync(string modName, int authorId, bool isSoftware = false);
        /// <summary>
        /// Gets mod by name.
        /// </summary>
        /// <param name="modName"></param>
        /// <returns></returns>
        Task<Mod> GetModByNameAsync(string modName);

        /// <summary>
        /// Gets the mods list async.
        /// </summary>
        /// <returns></returns>
        Task<Mod[]> GetModsListAsync(bool isSoftware = false);
        /// <summary>
        /// Remove mod.
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        Task<bool> RemoveModAsync(Mod mod);

        /// <summary>
        /// Updates the mod async.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="fileName">The mod file name.</param>
        /// <param name="version">The mod version.</param>
        /// <param name="updateTime">The update time.</param>
        /// <param name="isSoftware"></param>
        /// <returns></returns>
        Task<bool> UpdateModAsync(string modName, string fileName, string version, DateTimeOffset? updateTime = null, bool isSoftware = false);
        /// <summary>
        /// Updates mod file.
        /// </summary>
        /// <param name="modName"></param>
        /// <param name="newFile"></param>
        /// <returns></returns>
        Task<bool> UpdateModFileAsync(string modName, string newFile);
    }
}