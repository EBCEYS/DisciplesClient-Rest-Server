using Disciples2ApiModels.D2ApiModels;
using System.Text.Json;

namespace D2Launcher.Tools
{
    public static class ModsInfoStorage
    {
        public static List<InstalledModInfo> InstalledModsInfo { get; private set; } = new();

        public static bool RemoveMod(InstalledModInfo mod)
        {
            int removeResult = InstalledModsInfo.RemoveAll(m => m.ModInfo.Name == mod.ModInfo.Name && m.Path == mod.Path);
            if (removeResult > 0)
            {
                Directory.Delete(mod.Path, true);
                WriteInstalledModsInfo();
            }
            return removeResult > 0;
        }

        public static bool TryAdd(InstalledModInfo info)
        {
            if (!IsContains(info.ModInfo))
            {
                InstalledModsInfo.Add(info);
                WriteInfoToFile();
                return true;
            }
            return false;
        }

        public static bool IsContains(ModInfo info)
        {
            return InstalledModsInfo.Exists(x => x.ModInfo.Name == info.Name);
        }

        public static void ReadInstalledModsInfo()
        {
            if (File.Exists(Program.BaseModsInfoFilePath))
            {
                InstalledModsInfo = JsonSerializer.Deserialize<List<InstalledModInfo>>(File.ReadAllText(Program.BaseModsInfoFilePath), Program.SerializerOptions);
            }
        }

        public static void WriteInstalledModsInfo()
        {
            if (File.Exists(Program.BaseModsInfoFilePath))
            {
                File.Delete(Program.BaseModsInfoFilePath);
            }
            WriteInfoToFile();
        }

        private static void WriteInfoToFile()
        {
            string json = JsonSerializer.Serialize(InstalledModsInfo, Program.SerializerOptions);
            File.WriteAllText(Program.BaseModsInfoFilePath, json);
        }
    }

    public class InstalledModInfo
    {
        public ModInfo ModInfo { get; set; }
        public string Path { get; set; }

        public InstalledModInfo(ModInfo modInfo, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"\"{nameof(path)}\" не может быть пустым или содержать только пробел.", nameof(path));
            }

            ModInfo = modInfo;
            Path = path;
        }
        public InstalledModInfo()
        {

        }
    }
}
