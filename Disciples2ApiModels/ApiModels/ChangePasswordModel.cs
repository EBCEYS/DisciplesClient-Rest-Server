using System.ComponentModel.DataAnnotations;

namespace Disciples2ApiModels.ApiModels
{
    public struct ChangePasswordModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public ChangePasswordModel(string currentPassword, string newPassword) : this()
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                throw new ArgumentException($"\"{nameof(currentPassword)}\" не может быть пустым или содержать только пробел.", nameof(currentPassword));
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException($"\"{nameof(newPassword)}\" не может быть пустым или содержать только пробел.", nameof(newPassword));
            }

            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
        public ChangePasswordModel CreateModelWithHashedPasswords(Func<string, string> hashFunc)
        {
            if (hashFunc is null)
            {
                throw new ArgumentNullException(nameof(hashFunc));
            }
            return new()
            {
                CurrentPassword = hashFunc(this.CurrentPassword),
                NewPassword = hashFunc(this.NewPassword)
            };
        }
    }
}
