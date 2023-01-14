using System.Collections.Concurrent;
using System.Text.Json;

namespace AdminAndAuthorClient.UserDataStorage
{
    public static class UnZipedFilesDirectoryStorage
    {
        public static ConcurrentDictionary<string, string> ModsInfo { get; private set; } = new();

        public static void LoadGamesDirectories()
        {
            if (File.Exists(Program.GamesInfoDir))
            {
                ModsInfo = JsonSerializer.Deserialize<ConcurrentDictionary<string, string>>(File.ReadAllText(Program.GamesInfoDir), Program.SerializerOptions) ?? new();
            }
        }

        public static void CheckGamesExists()
        {
            foreach (KeyValuePair<string, string> mod in ModsInfo)
            {
                if (!Directory.Exists(mod.Value))
                {
                    ModsInfo.TryRemove(mod.Key, out _);
                    SaveGamesDirectories();
                }
            }
        }

        public static void SaveGamesDirectories()
        {
            try
            {
                if (File.Exists(Program.GamesInfoDir))
                {
                    File.Delete(Program.GamesInfoDir);
                }
                File.WriteAllText(Program.GamesInfoDir, JsonSerializer.Serialize(ModsInfo, Program.SerializerOptions));
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Can not save games info! {ex.Message} {ex.StackTrace}");
            }
        }
    }
}
