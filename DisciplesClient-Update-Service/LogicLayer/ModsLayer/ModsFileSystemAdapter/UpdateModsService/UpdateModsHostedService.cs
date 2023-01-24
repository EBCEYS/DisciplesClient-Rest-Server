using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service.DataBase.DataBaseAdapters.ModsDataBaseAdapter.Interfaces;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.Interfaces;
using NLog;

namespace DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.UpdateModsService
{
    /// <summary>
    /// The hosted service which updates mods files in data base.
    /// </summary>
    public class UpdateModsHostedService : IHostedService
    {
        private readonly Logger logger;
        private readonly IModsDBAdapter dbAdapter;
        private readonly IModsFSAdapter fsAdapter;

        private Timer updateModsTimer;
        /// <summary>
        /// Initiates update mods hosted service.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbAdapter">The data base adapter.</param>
        /// <param name="fsAdapter">The file system adapter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateModsHostedService(Logger logger, IModsDBAdapter dbAdapter, IModsFSAdapter fsAdapter)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbAdapter = dbAdapter ?? throw new ArgumentNullException(nameof(dbAdapter));
            this.fsAdapter = fsAdapter ?? throw new ArgumentNullException(nameof(fsAdapter));
        }
        /// <summary>
        /// Starts service.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                updateModsTimer = new(UpdateModServiceProcess, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            }, cancellationToken);
        }
        /// <summary>
        /// Stops service.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await updateModsTimer.DisposeAsync();
        }

        private void UpdateModServiceProcess(object state)
        {
            Task.Run(async () =>
            {
                Mod[] mods = await dbAdapter.GetModsListAsync();
                if (mods == null)
                {
                    return;
                }
                foreach (Mod mod in mods)
                {
                    if (string.IsNullOrEmpty(mod.FileName))
                    {
                        return;
                    }
                    try
                    {
                        await ProcessModAsync(mod);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error on processing update mod file!");
                        continue;
                    }
                }
            });
        }

        private async Task ProcessModAsync(Mod mod)
        {
            string[] files = await fsAdapter.GetModFilesAsync(mod.Name);
            if (files == null)
            {
                await dbAdapter.UpdateModFileAsync(mod.Name, "");
                return;
            }
            if (!files.Contains(mod.FileName))
            {
                FileInfo fi = await fsAdapter.GetLastModFileAsync(mod.Name);
                if (fi is not null)
                {
                    await dbAdapter.UpdateModFileAsync(mod.Name, fi.Name);
                    logger.Debug("Update mod {modName} file to {fiName}", mod.Name, fi.Name);
                }
            }
        }
    }
}
