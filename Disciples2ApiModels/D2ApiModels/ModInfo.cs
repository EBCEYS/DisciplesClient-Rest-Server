namespace Disciples2ApiModels.D2ApiModels
{
    public struct ModInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public ModInfo(string name, string version, string author)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException($"\"{nameof(version)}\" не может быть неопределенным или пустым.", nameof(version));
            }

            if (string.IsNullOrEmpty(author))
            {
                throw new ArgumentException($"\"{nameof(author)}\" не может быть неопределенным или пустым.", nameof(author));
            }

            Name = name;
            Version = version;
            Author = author;
        }
    }
}
