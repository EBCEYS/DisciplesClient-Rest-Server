using D2Launcher.Forms;
using D2Launcher.Tools;
using D2Launcher.Tools.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace D2Launcher
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LoadAppSettings();
            CreateModsDirIfNotExists();
            ModsInfoStorage.ReadInstalledModsInfo();
            Application.Run(new MainForm());
        }

        private static void CreateModsDirIfNotExists()
        {
            if (!Directory.Exists(BaseModsPath))
            {
                Directory.CreateDirectory(BaseModsPath);
            }
        }

        private static void LoadAppSettings()
        {
            string path = Path.Combine(BasePath, "config", "appsettings.json");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Can not find appsettings file!", path);
            }
            JsonObject json = JsonSerializer.Deserialize<JsonObject>(File.ReadAllBytes(path), SerializerOptions) ?? throw new Exception("Can not load appsettings!");
            Url = new(json[nameof(Url)].ToString());
        }

        public static JsonSerializerOptions SerializerOptions { get; } = new()
        {
            Converters = { new JsonStringEnumConverter() },
            WriteIndented = false,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };

        public static Uri Url { get; private set; }

        public static string BasePath => AppDomain.CurrentDomain.BaseDirectory;
        public static string BaseModsPath { get; } = Path.Combine(BasePath, "Mods");
        public static string BaseModsInfoFilePath { get; } = Path.Combine(BaseModsPath, "installedmodsinfo.json");

        public static string BaseSoftPath { get; } = Path.Combine(BasePath, "Softwares");
        public static string BaseSoftsInfoFilePath { get; } = Path.Combine(BaseSoftPath, "installedsoftsinfo.json");

        public static IHttpSender HttpSender { get; } = new HttpSender();
        public static string GetModDir(string modName)
        {
            return Path.Combine(BaseModsPath, modName);
        }
        public static string GetSoftDir(string softName)
        {
            return Path.Combine(BaseSoftPath, softName);
        }
    }
}