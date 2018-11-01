namespace Identity.Management.Client.Clients
{
    public class ClientModel
    {
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public ClientSecretModel[] ClientSecrets { get; set; }
        public string[] GrantTypes { get; set; }
        public string[] Scopes { get; set; }
    }
}