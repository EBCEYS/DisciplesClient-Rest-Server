using NLog;

namespace Disciples2.Info.InfoAdapter
{
    public class InfoAdapter : IInfoAdapter
    {
        public string? InfoCache { get; private set; }
        private string? InfoFilePath { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void SetInfoFilePath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            InfoFilePath = path;
        }

        public async Task UpdateInfoCacheAsync(CancellationToken token)
        {
            string? str = await File.ReadAllTextAsync(InfoFilePath ?? throw new Exception($"{nameof(InfoFilePath)} is not set!"), token);
            if (!string.IsNullOrWhiteSpace(str))
            {
                InfoCache = str;
            }
        }

        public async Task SetNewInfoAsync(string? newInfo)
        {
            await File.WriteAllTextAsync(InfoFilePath ?? throw new Exception(nameof(InfoFilePath)), newInfo);
        }
    }
}
