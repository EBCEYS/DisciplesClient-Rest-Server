using AdminAndAuthorClient.Http;
using AdminAndAuthorClient.UserDataStorage;
using AdminAndAuthorClient.UserForms.MainUserForm;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AdminAndAuthorClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            SetProps();
            LoadAppSettings();
            TryCreateGamesDir();
            Application.Run(new MainForm());
        }

        private static void SetProps()
        {
            SerializerOptions = new()
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = false,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            };
            GamesArchivesDir = Path.Combine(BasePath, "D2Archives");
            GamesInfoDir = Path.Combine(GamesArchivesDir, "archivesInfo.json");
        }

        private static void LoadAppSettings()
        {
            string path = Path.Combine(BasePath, "config", "appsettings.json");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Can not find appsettings file!", path);
            }
            JsonObject json = JsonSerializer.Deserialize<JsonObject>(File.ReadAllBytes(path), SerializerOptions);
            Url = new(json[nameof(Url)].ToString());
            UserStorage.TryLoadUserData();
            UnZipedFilesDirectoryStorage.LoadGamesDirectories();
            HttpSender = new HttpSender();
        }

        private static void TryCreateGamesDir()
        {
            if (!Directory.Exists(GamesArchivesDir))
            {
                Directory.CreateDirectory(GamesArchivesDir);
            }
        }
        public static JsonSerializerOptions SerializerOptions { get; private set; }

        public static string GamesInfoDir { get; private set; }
        public static string GamesArchivesDir { get; private set; }

        public static IHttpSender HttpSender { get; private set; }

        public static string BasePath => AppDomain.CurrentDomain.BaseDirectory;

        public static Uri Url { get; private set; }
    }
}