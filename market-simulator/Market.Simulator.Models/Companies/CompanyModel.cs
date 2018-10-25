using Market.Simulator.Models.Common;

namespace Market.Simulator.Models.Companies
{
    public class CompanyModel : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}