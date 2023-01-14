using DataBase.DataBaseAdapters.UsersDataBaseAdapter.Interface;
using Disciples2ApiModels.ApiModels;
using Disciples2ClientDataBaseLibrary.DataBase;
using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic.Exceptions;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Collections.Concurrent;
using UserCache;

namespace DataBase.DataBaseAdapters.UsersDataBaseAdapter;

/// <summary>
/// The users db adapter.
/// </summary>
public class UsersDBAdapter : IUsersDBAdapter
{
    private readonly Logger logger;
    private readonly IUsersCacheAdapter cacheAdapter;

    /// <summary>
    /// Ctor for users db adapter.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="cacheAdapter"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public UsersDBAdapter(Logger logger, IUsersCacheAdapter cacheAdapter)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.cacheAdapter = cacheAdapter ?? throw new ArgumentNullException(nameof(cacheAdapter));
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
            User u = await db.Users.FirstOrDefaultAsync(u => u.IsActive && u.UserName == username && u.Password == password);
            cacheAdapter.AddOrReplaceUser(u);
            return u;
        }
        catch(Exception ex)
        {
            logger.Error(ex, "Error on trying to login async!");
            return null;
        }
    }
    /// <summary>
    /// Check if user exists.
    /// </summary>
    /// <param name="username">The user name.</param>
    /// <param name="roles">The user roles.</param>
    /// <param name="password">The user password.</param>
    /// <returns>true if exists; otherwise false.</returns>
    public async Task<bool> CheckUserExistsAsync(string username, string[] roles, string password)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            return await db.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Roles == roles && x.Password == password) != null;
        }
        catch(Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// Check if user exists.
    /// </summary>
    /// <param name="username">The user name.</param>
    /// <param name="roles">The user roles.</param>
    /// <param name="password">The user password.</param>
    /// <returns>true if exists; otherwise false.</returns>
    public bool CheckUserExists(string username, string[] roles, string password)
    {
        try
        {
            using Disciples2ClientDBConnext db = new();
            return db.Users.FirstOrDefault(x => x.UserName == username && x.Roles == roles && x.Password == password) != null;
        }
        catch (Exception)
        {
            return false;
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
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
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
            if ((await db.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName)) == null)
            {
                await db.AddAsync(user);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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
            User u = await db.Users.FirstAsync(u => u.Id == id && u.IsActive);
            u.IsActive = false;
            await db.SaveChangesAsync();
            cacheAdapter.RemoveUser(u);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on deleting new user!");
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
    public async Task<Mod[]> GetAuthorsModsAsync(int id)
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
    public async Task<Mod[]> GetAuthorsModsAsync(string name)
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
    /// <summary>
    /// Changes the user password by id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="model">The change password model.</param>
    /// <returns>true if changed; otherwise false.</returns>
    public async Task<bool> ChangePasswordAsync(int id, ChangePasswordModel model)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            User u = (await db.Users.FirstOrDefaultAsync(usr => usr.Id == id && usr.Password == model.CurrentPassword));
            u.Password = model.NewPassword;
            await db.SaveChangesAsync();
            cacheAdapter.AddOrReplaceUser(u);
            return true;
        }
        catch(Exception ex)
        {
            logger.Error(ex, "Error on changin password!");
            return false;
        }
    }
    /// <summary>
    /// Changes the user email by id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="model">The change email model.</param>
    /// <returns>true if changed; otherwise false.</returns>
    public async Task<bool> ChangeEmailAsync(int id, ChangeEmailModel model)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            User u = (await db.Users.FirstOrDefaultAsync(usr => usr.Id == id));
            if (string.IsNullOrEmpty(u.Email) || u.Email == model.CurrentEmail)
            {
                u.Email = model.NewEmail;
            }
            else
            {
                throw new UserNotFoundException();
            }
            await db.SaveChangesAsync();
            cacheAdapter.AddOrReplaceUser(u);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on changin password!");
            return false;
        }
    }
    /// <summary>
    /// Changes the user username by id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="model">The change username model.</param>
    /// <returns>true if changed; otherwise false.</returns>
    public async Task<bool> ChangeUserNameAsync(int id, ChangeUserNameModel model)
    {
        try
        {
            await using Disciples2ClientDBConnext db = new();
            User u = (await db.Users.FirstOrDefaultAsync(usr => usr.Id == id && usr.UserName == model.CurrentUserName));
            u.UserName = model.NewUserName;
            await db.SaveChangesAsync();
            cacheAdapter.AddOrReplaceUser(u);
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error on changin password!");
            return false;
        }
    }
}
