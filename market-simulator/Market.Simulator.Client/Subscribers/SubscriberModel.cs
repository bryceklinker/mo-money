using Market.Simulator.Client.Common;

namespace Market.Simulator.Client.Subscribers
{
    public class SubscriberModel : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}