using Disciples2ClientDataBaseLibrary.DBModels;
using DisciplesClient_Update_Service;
using Microsoft.EntityFrameworkCore;

namespace Disciples2ClientDataBaseLibrary.DataBase;

/// <summary>
/// The db context.
/// </summary>
public class Disciples2ClientDBConnext : DbContext, IDisposable
{
    /// <summary>
    /// The users.
    /// </summary>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// The ctor for db context.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public Disciples2ClientDBConnext(DbContextOptions<Disciples2ClientDBConnext> options)
        : base(options) { }
    /// <summary>
    /// 
    /// </summary>
    public Disciples2ClientDBConnext() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Program.UsersDBConnectionString);
    }
}
