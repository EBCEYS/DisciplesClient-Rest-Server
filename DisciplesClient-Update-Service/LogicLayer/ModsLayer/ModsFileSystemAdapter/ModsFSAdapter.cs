using DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.Interfaces;
using NLog;
using System.Collections.Concurrent;

namespace DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter
{
    /// <summary>
    /// The mods file system adapter.
    /// </summary>
    public class ModsFSAdapter : IModsFSAdapter
    {
        private readonly Logger logger;
        private static string BaseModsPath => Program.ModsDirBasePath;
        private readonly ConcurrentQueue<string> removeFileQueue;
        /// <summary>
        /// Initiates the mods file system adapter.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="removeFileQueue"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ModsFSAdapter(Logger logger, ConcurrentQueue<string> removeFileQueue)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.removeFileQueue = removeFileQueue ?? throw new ArgumentNullException(nameof(removeFileQueue));
        }
        /// <summary>
        /// Creates the new mod file.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="mod">The mod.</param>
        /// <returns>true if created; otherwise false.</returns>
        public async Task<bool> UploadModAsync(string modName, string fileName, MemoryStream mod)
        {
            try
            {
                if (!Directory.Exists(Path.Combine(BaseModsPath, modName)))
                {
                    Directory.CreateDirectory(Path.Combine(BaseModsPath, modName));
                }
                string path = Path.Combine(BaseModsPath, modName, fileName);
                if (File.Exists(path))
                {
                    throw new Exception("File already exists!");
                }
                await File.WriteAllBytesAsync(path, mod.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on creating new mod!");
                return false;
            }
        }

        /// <summary>
        /// Writes remove mod to remove mod queue.
        /// </summary>
        /// <param name="modDir">The mod directory.</param>
        /// <param name="fileName">The mod file name.</param>
        /// <returns>true if file exists and will be deleted; otherwise false.</returns>
        public bool RemoveMode(string modDir, string fileName)
        {
            string path = Path.Combine(BaseModsPath, modDir, fileName);
            if (File.Exists(path))
            {
                removeFileQueue.Enqueue(path);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the mod file.
        /// </summary>
        /// <param name="modName">The mod name (directory).</param>
        /// <param name="fileName">The mod file name.</param>
        /// <returns>The stream from <see cref="StreamReader"/> if file exists; otherwise false.</returns>
        public async Task<Stream> GetModFileAsync(string modName, string fileName)
        {
            return await Task.Run(() => 
            { 
                try
                {
                    string path = Path.Combine(BaseModsPath, modName, fileName);
                    if (File.Exists(path))
                    {
                        StreamReader sr = new(path);
                        return sr.BaseStream;
                    }
                    throw new FileNotFoundException(path);
                }
                catch(Exception ex)
                {
                    logger.Error(ex, "Error on geting mod file!");
                    return null;
                }
            });
        }
    }
}
