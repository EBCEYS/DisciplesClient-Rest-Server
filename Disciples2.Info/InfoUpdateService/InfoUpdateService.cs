using Disciples2.Info.InfoAdapter;
using Microsoft.Extensions.Hosting;
using NLog;

namespace Disciples2.Info.InfoUpdateService
{
    public class InfoUpdateService : IHostedService, IDisposable
    {
        private readonly Logger? logger;
        private readonly IInfoAdapter? infoAdapter;
        private Timer? updateInfoTimer;

        public InfoUpdateService(Logger? logger, IInfoAdapter? infoAdapter)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.infoAdapter = infoAdapter ?? throw new ArgumentNullException(nameof(infoAdapter));
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger?.Info("Starting update info service");
            await Task.Run(() => 
            {
                updateInfoTimer = new(UpdateTimerProcess, cancellationToken, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            }, cancellationToken);
        }

        public void UpdateTimerProcess(object? state)
        {
            try
            {
                if (state is null)
                {
                    throw new ArgumentNullException(nameof(state));
                }
                CancellationToken token = (CancellationToken)state;
                infoAdapter?.UpdateInfoCacheAsync(token).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                logger?.Error(ex, "Error on processing info update!");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            infoAdapter?.Dispose();
            updateInfoTimer?.Dispose();
        }
    }
}
