using Disciples2ApiModels.ApiModels;

namespace AdminAndAuthorClient.UserForms.ChangeForms
{
    public class ChangeModelFactory
    {
        private readonly ChangeTypes type;
        private readonly string val1;
        private readonly string val2;

        public ChangeModelFactory(ChangeTypes type, string val1, string val2)
        {
            if (string.IsNullOrEmpty(val2))
            {
                throw new ArgumentException($"\"{nameof(val2)}\" не может быть неопределенным или пустым.", nameof(val2));
            }

            this.type = type;
            this.val1 = val1 ?? throw new ArgumentNullException(nameof(val1));
            this.val2 = val2;
        }

        public object Create()
        {
            if (type == ChangeTypes.password)
            {
                return new ChangePasswordModel(val1, val2);
            }
            else if (type == ChangeTypes.email)
            {
                return new ChangeEmailModel(val1, val2);
            }
            else if (type == ChangeTypes.username)
            {
                return new ChangeUserNameModel(val2, val1);
            }
            throw new NotImplementedException($"Unsupported type: {type}!");
        }
    }
}
