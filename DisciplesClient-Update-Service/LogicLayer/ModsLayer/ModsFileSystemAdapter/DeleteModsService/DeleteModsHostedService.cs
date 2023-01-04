using NLog;
using System.Collections.Concurrent;
using System.Text.Json;

namespace DisciplesClient_Update_Service.LogicLayer.ModsLayer.ModsFileSystemAdapter.DeleteModsService
{
    /// <summary>
    /// The hosted service deletes the mods.
    /// </summary>
    public class DeleteModsHostedService : IHostedService
    {
        private Timer deleteFileTimer;
        private readonly ConcurrentQueue<string> removeFileQueue;
        private readonly Logger logger;
        private static string QueueFilePath => Program.RemoveQueueFilePath;
        /// <summary>
        /// Initiates the delete mods hosted service.
        /// </summary>
        /// <param name="removeFileQueue">The remove file queue.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DeleteModsHostedService(ConcurrentQueue<string> removeFileQueue, Logger logger)
        {
            this.removeFileQueue = removeFileQueue ?? throw new ArgumentNullException(nameof(removeFileQueue));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <param name="cancellationToken">The token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (File.Exists(QueueFilePath))
            {
                try
                {
                    using FileStream reader = new(QueueFilePath, FileMode.Open, FileAccess.Read);
                    ConcurrentQueue<string> readQueue = await JsonSerializer.DeserializeAsync<ConcurrentQueue<string>>(reader, cancellationToken: cancellationToken);
                    while (readQueue.TryDequeue(out string path))
                    {
                        removeFileQueue.Enqueue(path);
                    }
                }
                catch(Exception ex)
                {
                    logger.Error(ex, "Error on reading queue file!");
                }
            }
            await Task.Run(() =>
            {
                deleteFileTimer = new(RemovingModsProcess, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            }, cancellationToken);
        }
        /// <summary>
        /// Stops the service.
        /// </summary>
        /// <param name="cancellationToken">The token.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await deleteFileTimer.DisposeAsync();
            
                if (File.Exists(QueueFilePath))
                {
                    File.Delete(QueueFilePath);
                }
                string jsonedQueue = JsonSerializer.Serialize(removeFileQueue);
                await File.WriteAllTextAsync(QueueFilePath, jsonedQueue, cancellationToken);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on stoping delete mods service!");
            }
        }

        private static bool RemoveMod(string path)
        {
            FileInfo fi = new(path);
            if (fi.Exists)
            {
                try
                {
                    fi.Delete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                throw new FileNotFoundException(path);
            }
        }

        private void RemovingModsProcess(object state)
        {
            while (removeFileQueue.TryDequeue(out string file))
            {
                Task.Run(() =>
                {
                    try
                    {
                        bool result = RemoveMod(file);
                        if (!result)
                        {
                            removeFileQueue.Enqueue(file);
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        logger.Error(ex, $"File does not exists {file}");
                    }
                });
            }
        }
    }
}
