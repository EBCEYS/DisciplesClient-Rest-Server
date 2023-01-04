namespace AdminAndAuthorClient.UserDataStorage
{
    public static class UserStorage
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string Token { get; set; }
        public static string SavedDataPath { get; } = Path.Combine(Program.BasePath, "config", "userdata");

        public static bool IsParamsSet()
        {
            return !(string.IsNullOrEmpty(Token));
        }

        public static void SetUserInfo(string userName, string password, string token)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException($"\"{nameof(userName)}\" не может быть неопределенным или пустым.", nameof(userName));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"\"{nameof(password)}\" не может быть неопределенным или пустым.", nameof(password));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException($"\"{nameof(token)}\" не может быть неопределенным или пустым.", nameof(token));
            }

            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Token = token ?? throw new ArgumentNullException(nameof(token));
            TrySaveUserData();
        }

        private static void TrySaveUserData()
        {
            try
            {
                if (File.Exists(SavedDataPath))
                {
                    File.Delete(SavedDataPath);
                }
                File.WriteAllText(SavedDataPath, Token);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error on saving user data!{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }
        public static void TryLoadUserData()
        {
            try
            {
                if (File.Exists(SavedDataPath))
                {
                    Token = File.ReadAllText(SavedDataPath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error on loading user data!{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }
        }
    }
}
