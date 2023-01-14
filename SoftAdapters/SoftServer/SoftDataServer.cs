using Microsoft.AspNetCore.Http;
using NLog;

namespace SoftAdapters.SoftServer
{
    public class SoftDataServer
    {
        private readonly Logger logger;

        public SoftDataServer(Logger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> UploadModAsync(string softName, string version, string authorName, IFormFile mod)
        {
            DateTimeOffset uploadTime = DateTimeOffset.UtcNow;
        }
    }
}
