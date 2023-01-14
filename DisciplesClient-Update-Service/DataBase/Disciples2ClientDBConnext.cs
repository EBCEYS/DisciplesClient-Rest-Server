using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service;
using Microsoft.EntityFrameworkCore;

namespace Disciples2ClientDataBaseLibrary.DataBase;

/// <summary>
/// The db context.
/// </summary>
public class Disciples2ClientDBConnext : DbContext, IDisposable, IAsyncDisposable
{
    /// <summary>
    /// The users.
    /// </summary>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// The mods.
    /// </summary>
    public DbSet<Mod> Mods { get; set; }
    /// <summary>
    /// The ctor for db context.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public Disciples2ClientDBConnext(DbContextOptions<Disciples2ClientDBConnext> options)
        : base(options) 
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    /// <summary>
    /// 
    /// </summary>
    public Disciples2ClientDBConnext() 
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);

        modelBuilder.Entity<Mod>().HasKey(mod => mod.Name);
        modelBuilder.Entity<Mod>()
            .HasOne(p => p.Author)
            .WithMany(t => t.Mods)
            .HasForeignKey(p => p.AuthorUserId);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Добавил дефолтное значение для тестов (лень mock класс добавлять)
        optionsBuilder.UseNpgsql(Program.D2DBConnectionString 
            ?? "Host=localhost;Port=5433;Database=Disciples2ClientDB;Username=ebcey;Password=123");
    }
}
