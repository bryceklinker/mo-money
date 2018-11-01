namespace Identity.Management.Client.Clients
{
    public static class SupportedGrantTypes
    {
        public const string Password = "password";
        public const string ClientCredentials = "client_credentials";

        public static readonly string[] All =
        {
            Password,
            ClientCredentials
        };
    }
}