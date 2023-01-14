using Disciples2ClientDataBaseLibrary.DataBase;
using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service.DataBase.DataBaseAdapters.ModsDataBaseAdapter.Interfaces;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.Exceptions;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DisciplesClient_Update_Service.DataBase.DataBaseAdapters.ModsDataBaseAdapter
{
    /// <summary>
    /// The mods db adapter.
    /// </summary>
    public class ModsDBAdapter : IModsDBAdapter
    {
        private readonly Logger logger;
        /// <summary>
        /// Initiates the mods data base adapter.
        /// </summary>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ModsDBAdapter(Logger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Writes the new mod information to database.
        /// </summary>
        /// <param name="mod">The mod.</param>
        /// <returns>true if added successfuly; otherwise false.</returns>
        public async Task<bool> CreateModAsync(Mod mod)
        {
            try
            {
                await using Disciples2ClientDBConnext db = new();
                await db.Mods.AddAsync(mod);
                User user = await db.Users.FirstOrDefaultAsync(u => u.Id == mod.AuthorUserId);
                if (user.Mods == null)
                {
                    user.Mods = new()
                    {
                        mod
                    };
                }
                else
                    user.Mods.Add(mod);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on creating new mod!");
                return false;
            }
        }
        /// <summary>
        /// Updates the mod information.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="fileName"></param>
        /// <param name="version">The new mod version.</param>
        /// <param name="updateTime">The update time.</param>
        /// <param name="isSoftware"></param>
        /// <returns>true if updated successfuly; otherwise false.</returns>
        /// <exception cref="WrongModUpdateDateTimeException"/>
        public async Task<bool> UpdateModAsync(string modName, string fileName, string version, DateTimeOffset? updateTime = null, bool isSoftware = false)
        {
            if (updateTime == null)
            {
                updateTime = DateTime.UtcNow;
            }
            try
            {
                await using Disciples2ClientDBConnext db = new();
                Mod mod = await db.Mods.FirstOrDefaultAsync(mod => mod.Name == modName && mod.IsSoftware == isSoftware);
                if (mod == null)
                {
                    return false;
                }
                if (mod.LastUpdateDateTime > updateTime)
                {
                    throw new WrongModUpdateDateTimeException(mod.LastUpdateDateTime, updateTime.Value);
                }
                mod.LastUpdateDateTime = updateTime.Value;
                mod.Version = version;
                mod.FileName = fileName;
                await db.SaveChangesAsync();
                return true;
            }
            catch (WrongModUpdateDateTimeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on updating mod!");
                return false;
            }
        }
        /// <summary>
        /// Gets the mod's list.
        /// </summary>
        /// <returns>The mod list; null if catched any exceptions.</returns>
        public async Task<Mod[]> GetModsListAsync(bool isSoftware = false)
        {
            try
            {
                await using Disciples2ClientDBConnext db = new();
                Mod[] mods = (await db.Mods.Where(m => m.IsSoftware == isSoftware).ToListAsync()).ToArray();
                return mods;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on creating new mod!");
                return null;
            }
        }
        /// <summary>
        /// Gets the mod by mod name and author name.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="authorName">The author name.</param>
        /// <param name="isSoftware">The is software.</param>
        /// <returns>The mod if exists; otherwise null.</returns>
        public async Task<Mod> GetModByAuthorAsync(string modName, string authorName, bool isSoftware = false)
        {
            try
            {
                await using Disciples2ClientDBConnext db = new();
                return await db.Mods.FirstOrDefaultAsync(m => m.Name == modName && m.Author.UserName == authorName && m.IsSoftware == isSoftware);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on geting mod by author!");
                return null;
            }
        }
        /// <summary>
        /// Gets the mod by mod name and author id.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="authorId">The author id.</param>
        /// <param name="isSoftware"></param>
        /// <returns>The mod if exists; otherwise null.</returns>
        public async Task<Mod> GetModByAuthorAsync(string modName, int authorId, bool isSoftware = false)
        {
            try
            {
                await using Disciples2ClientDBConnext db = new();
                return await db.Mods.FirstOrDefaultAsync(m => m.Name == modName && m.AuthorUserId == authorId && m.IsSoftware == isSoftware);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on geting mod by author!");
                return null;
            }
        }
        /// <summary>
        /// Gets the mod by mod name.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <returns>The mod if exists; otherwise null.</returns>
        public async Task<Mod> GetModByNameAsync(string modName)
        {
            try
            {
                await using Disciples2ClientDBConnext db = new();
                return await db.Mods.FirstOrDefaultAsync(m => m.Name == modName);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on geting mod by author!");
                return null;
            }
        }
        /// <summary>
        /// Removes the mod async.
        /// </summary>
        /// <param name="mod">The mod.</param>
        /// <returns>true if deleted successfuly; otherwise false.</returns>
        public async Task<bool> RemoveModAsync(Mod mod)
        {
            try
            {
                await using Disciples2ClientDBConnext db = new();
                db.Mods.Remove(mod);
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on removing mod!");
                return false;
            }
        }
        /// <summary>
        /// Updates mod file name.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="newFile">The new file name.</param>
        /// <returns>true if success; otherwise false.</returns>
        public async Task<bool> UpdateModFileAsync(string modName, string newFile)
        {
            try
            {
                await using Disciples2ClientDBConnext db = new();
                (await db.Mods.FirstOrDefaultAsync(mod => mod.Name == modName)).FileName = newFile;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on removing mod!");
                return false;
            }
        }
    }
}
