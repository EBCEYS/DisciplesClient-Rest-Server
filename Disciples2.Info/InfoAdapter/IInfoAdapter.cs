namespace Disciples2.Info.InfoAdapter
{
    public interface IInfoAdapter : IDisposable
    {
        string? InfoCache { get; }

        void SetInfoFilePath(string? path);
        Task SetNewInfoAsync(string? newInfo);
        Task UpdateInfoCacheAsync(CancellationToken token);
    }
}