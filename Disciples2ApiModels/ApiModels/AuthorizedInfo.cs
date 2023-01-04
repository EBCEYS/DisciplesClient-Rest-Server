namespace Disciples2ApiModels.ApiModels
{
    [Serializable]
    public struct AuthorizedInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
