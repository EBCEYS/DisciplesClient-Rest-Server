using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ApiModels.D2ApiModels;
using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service.DataBase.DataBaseAdapters.ModsDataBaseAdapter.Interfaces;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.Interfaces;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.Interfaces;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic.Exceptions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using NLog;
using System.Reflection.Metadata.Ecma335;

namespace DisciplesClient_Update_Service.LogicLayer.ModsLayer
{
    /// <summary>
    /// The mods data server.
    /// </summary>
    public class ModsDataServer : IModsDataServer
    {
        private readonly Logger logger;
        private readonly IModsFSAdapter modsfsAdapter;
        private readonly IModsDBAdapter modsDBAdapter;
        private readonly IUsersDBAdapter usersDBAdapter;

        /// <summary>
        /// Initiates the mods data server.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="modsfsAdapter">The mods file system database adapter.</param>
        /// <param name="modsDBAdapter">The mods data base adapter.</param>
        /// <param name="usersDBAdapter">The users db adapter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ModsDataServer(Logger logger, IModsFSAdapter modsfsAdapter, IModsDBAdapter modsDBAdapter, IUsersDBAdapter usersDBAdapter)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.modsfsAdapter = modsfsAdapter ?? throw new ArgumentNullException(nameof(modsfsAdapter));
            this.modsDBAdapter = modsDBAdapter ?? throw new ArgumentNullException(nameof(modsDBAdapter));
            this.usersDBAdapter = usersDBAdapter ?? throw new ArgumentNullException(nameof(usersDBAdapter));
        }

        /// <summary>
        /// Gets the mod list async.
        /// </summary>
        /// <returns>The mods array; if catched any exceptions: null.</returns>
        public async Task<ModInfo[]> GetModsNamesAsync()
        {
            Mod[] modsArray = await modsDBAdapter.GetModsListAsync();
            if (modsArray == null)
            {
                return null;
            }
            List<ModInfo> modInfos = new();
            foreach(Mod mod in modsArray)
            {
                User author = await usersDBAdapter.GetUserByIdAsync(mod.AuthorUserId);
                modInfos.Add(new(mod.Name, mod.Version, author?.UserName ?? "unknown author"));
            }
            return modInfos.ToArray();
        }
        /// <summary>
        /// Uploads the mod async.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="version">The mod version.</param>
        /// <param name="authorName">The author name.</param>
        /// <param name="mod">The mod file.</param>
        /// <param name="updateDateTime">The update mod date time.</param>
        /// <returns></returns>
        /// <exception cref="UserNotFoundException"></exception>
        public async Task<bool> UploadModAsync(string modName, string version, string authorName, IFormFile mod, DateTimeOffset? updateDateTime = null)
        {
            try
            {
                if (updateDateTime == null)
                {
                    updateDateTime = DateTimeOffset.Now;
                }
                User author = await usersDBAdapter.GetUserByUserNameAsync(authorName) ?? throw new UserNotFoundException();
                Mod modDB = await modsDBAdapter.GetModByAuthorAsync(modName, author.Id);
                if (await ProcessModFileAsync(mod, modName))
                {
                    if (modDB != null)
                    {
                        return await modsDBAdapter.UpdateModAsync(modName, mod.FileName, version, updateDateTime);
                    }
                    else
                    {
                        modDB = new()
                        {
                            AuthorUserId = author.Id,
                            //Author = author,
                            Name = modName,
                            Version = version,
                            FirstUpdateDateTime = DateTime.Now,
                            LastUpdateDateTime = DateTime.Now,
                            FileName = mod.FileName
                        };
                        return await modsDBAdapter.CreateModAsync(modDB);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on uploading mod!");
                return false;
            }
        }

        private async Task<bool> ProcessModFileAsync(IFormFile mod, string modName)
        {
            if (mod is null)
            {
                throw new ArgumentNullException(nameof(mod));
            }

            if (string.IsNullOrEmpty(modName))
            {
                throw new ArgumentException($"\"{nameof(modName)}\" не может быть неопределенным или пустым.", nameof(modName));
            }

            if (string.Compare(".zip", Path.GetExtension(mod.FileName), StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new ArgumentException($"Wrong file extension! File extension should be \".zip\"!");
            }
            using MemoryStream stream = new();

            await mod.CopyToAsync(stream);

            stream.Seek(0, SeekOrigin.Begin);
            return await modsfsAdapter.UploadModAsync(modName, mod.FileName, stream);
        }

        /// <summary>
        /// Gets the mod file.
        /// </summary>
        /// <param name="modName">The mod name</param>
        /// <returns>The file stream if exists; otherwise false.</returns>
        public async Task<Stream> GetModFile(string modName)
        {
            try
            {
                if (string.IsNullOrEmpty(modName))
                {
                    throw new ArgumentNullException(nameof(modName));
                }
                Mod mod = await modsDBAdapter.GetModByNameAsync(modName);
                if (mod == null)
                {
                    return null;
                }
                return await modsfsAdapter.GetModFileAsync(modName, mod.FileName);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on geting mod file!");
                return null;
            }
        }
        /// <summary>
        /// Removes the mod file.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="fileName">The mod file name.</param>
        /// <returns>true if mod file will be deleted; otherwise false.</returns>
        public async Task<bool> RemoveModAsync(string modName, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(modName))
                {
                    throw new ArgumentException($"\"{nameof(modName)}\" не может быть неопределенным или пустым.", nameof(modName));
                }
                Mod mod = await modsDBAdapter.GetModByNameAsync(modName);
                if (mod == null)
                {
                    return false;
                }
                bool removeFromFSResult = modsfsAdapter.RemoveMode(modName, fileName);
                if (removeFromFSResult && mod.FileName == fileName)
                {
                    bool removeFromDBResult = await modsDBAdapter.RemoveModAsync(mod);
                    return removeFromDBResult && removeFromFSResult;
                }
                return removeFromFSResult;
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on removing mod!");
                return false;
            }
        }
    }
}
