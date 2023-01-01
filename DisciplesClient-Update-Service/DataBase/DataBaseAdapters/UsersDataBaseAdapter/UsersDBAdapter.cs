using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ClientDataBaseLibrary.DataBase;
using Disciples2ClientDataBaseModels.DBModels;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DataBase.DataBaseAdapters.UsersDataBaseAdapter;

/// <summary>
/// The users db adapter.
/// </summary>
public class UsersDBAdapter : IUsersDBAdapter
{
    private readonly Logger logger;

    /// <summary>
    /// Ctor for users db adapter.
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public UsersDBAdapter(Logger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    /// <summary>
    /// Logins the user if exists.
    /// </summary>
    /// <param name="username">The user name.</param>
    /// <param name="password">The password.</param>
    /// <returns>The user if exists; otherwise null.</returns>
    public async Task<User> LoginUserAsync(string username, string password)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            return await db.Users.Where(u => u.IsActive && u.UserName == username && u.Password == password).FirstAsync();
        }
        catch(Exception ex)
        {
            logger.Error(ex, "Error on trying to login async!");
            return null;
        }
    }
    /// <summary>
    /// Gets user by id if exists.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <returns>The user if exists; otherwise null.</returns>
    public async Task<User> GetUserByIdAsync(int id)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            return await db.Users.Where(u => u.Id == id).FirstAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on getting users from data base!");
            return null;
        }
    }
    /// <summary>
    /// Creates the user if not exists.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>true if created; otherwise false.</returns>
    public async Task<bool> CreateUserAsync(User user)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            await db.AddAsync(user);
            await db.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            logger.Error(ex, "Error on creating new user!");
            return false;
        }
    }
    /// <summary>
    /// Deletes the user by id if exists (this method is not actualy deletes the user. its just marks the user to be deleted).
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <returns>true if deleted; otherwise false.</returns>
    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            (await db.Users.FirstAsync(u => u.Id == id && u.IsActive)).IsActive = false;
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on creating new user!");
            return false;
        }
    }
    /// <summary>
    /// Gets the user by username async.
    /// </summary>
    /// <param name="name">The user name.</param>
    /// <returns>The user if exists; otherwise null.</returns>
    public async Task<User> GetUserByUserNameAsync(string name)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            return await db.Users.Where(u => u.UserName == name).FirstAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on getting users from data base!");
            return null;
        }
    }

    /// <summary>
    /// Gets the author mods.
    /// </summary>
    /// <param name="id">The author id.</param>
    /// <returns>Mods array if exists; otherwise null.</returns>
    public async Task<Mod[]> GetAuthorsMods(int id)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            return (await db.Users.Where(u => u.Id == id).FirstOrDefaultAsync()).Mods.ToArray();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on geting mods list by user id!");
            return null;
        }
    }
    /// <summary>
    /// Gets the author mods.
    /// </summary>
    /// <param name="name">The author username.</param>
    /// <returns>Mods array if exists; otherwise null.</returns>
    public async Task<Mod[]> GetAuthorsMods(string name)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            return (await db.Users.Where(u => u.UserName == name).FirstOrDefaultAsync()).Mods.ToArray();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on geting mods list by user name!");
            return null;
        }
    }
}
