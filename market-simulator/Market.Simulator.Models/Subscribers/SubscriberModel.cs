using Market.Simulator.Models.Common;

namespace Market.Simulator.Models.Subscribers
{
    public class SubscriberModel : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}