using System.IO.Compression;

namespace AdminAndAuthorClient.UserDataStorage
{
    public static class UnZiper
    {
        public static void ProcessModFile(string modName, string archive, string extractPath)
        {
            try
            {
                if (!File.Exists(archive))
                {
                    throw new FileNotFoundException($"Can not find archive file {archive}!");
                }
                UnZip(archive, extractPath);
                UnZipedFilesDirectoryStorage.ModsInfo.TryAdd(modName, extractPath);
                UnZipedFilesDirectoryStorage.SaveGamesDirectories();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error on processing archive!{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }

        private static void UnZip(string archive, string extractPath)
        {
            ZipFile.ExtractToDirectory(archive, extractPath, true);
            File.Delete(archive);
        }
    }
}
