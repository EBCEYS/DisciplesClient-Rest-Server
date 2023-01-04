namespace Disciples2ApiModels.ApiModels
{
    public struct ChangeEmailModel
    {
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
        public ChangeEmailModel(string currentEmail, string newEmail) : this()
        {
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                throw new ArgumentException($"\"{nameof(newEmail)}\" не может быть пустым или содержать только пробел.", nameof(newEmail));
            }

            CurrentEmail = currentEmail;
            NewEmail = newEmail;
        }
    }
}
