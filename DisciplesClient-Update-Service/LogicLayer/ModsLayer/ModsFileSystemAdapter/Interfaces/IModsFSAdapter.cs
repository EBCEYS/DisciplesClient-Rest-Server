namespace DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.Interfaces
{
    /// <summary>
    /// The mods file system adapter.
    /// </summary>
    public interface IModsFSAdapter
    {
        /// <summary>
        /// Gets the mod file.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns></returns>
        Task<Stream> GetModFileAsync(string modName, string fileName);
        Task<string[]> GetModFilesAsync(string modName);

        /// <summary>
        /// Removes the mod.
        /// </summary>
        /// <param name="modDir">The mod dir.</param>
        /// <param name="modname">The mod file name.</param>
        bool RemoveMode(string modDir, string modname);
        /// <summary>
        /// Uploads the mod.
        /// </summary>
        /// <param name="modDir">The mod directory.</param>
        /// <param name="modName">The mod file name.</param>
        /// <param name="mod">The mod file content.</param>
        /// <returns></returns>
        Task<bool> UploadModAsync(string modDir, string modName, MemoryStream mod);
    }
}