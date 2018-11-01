using Market.Simulator.Client.Common;

namespace Market.Simulator.Client.Companies
{
    public class CompanyModel : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}