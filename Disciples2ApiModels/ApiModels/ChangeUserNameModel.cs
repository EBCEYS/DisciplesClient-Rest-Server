using System.ComponentModel.DataAnnotations;

namespace Disciples2ApiModels.ApiModels
{
    public struct ChangeUserNameModel
    {
        [Required]
        public string CurrentUserName { get; set; }
        [Required]
        public string NewUserName { get; set; }

        public ChangeUserNameModel(string newUserName, string currentUserName) : this()
        {
            if (string.IsNullOrWhiteSpace(newUserName))
            {
                throw new ArgumentException($"\"{nameof(newUserName)}\" не может быть пустым или содержать только пробел.", nameof(newUserName));
            }

            if (string.IsNullOrWhiteSpace(currentUserName))
            {
                throw new ArgumentException($"\"{nameof(currentUserName)}\" не может быть пустым или содержать только пробел.", nameof(currentUserName));
            }

            NewUserName = newUserName;
            CurrentUserName = currentUserName;
        }
    }
}
