namespace Disciples2ApiModels.ApiModels
{
    [Serializable]
    public struct AuthorizedInfo
    {
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
