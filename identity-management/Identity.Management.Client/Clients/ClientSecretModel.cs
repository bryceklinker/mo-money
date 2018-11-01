using System;

namespace Identity.Management.Client.Clients
{
    public class ClientSecretModel
    {
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
    }
}