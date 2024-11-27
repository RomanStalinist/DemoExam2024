using System.Linq;

namespace Demo1.Data
{
    public partial class Partner
    {
        public string TypeAndName => $"{Type} | {Title}";
        public int Discount
        {
            get
            {
                var sum = App.Context.PartnerProducts.Where(pp => pp.PartnerID == ID)
                    .Select(pp => pp.CountProduct)
                    .DefaultIfEmpty(0)
                    .Sum();
                if (sum >= 10000 && sum < 49999) return 5;
                if (sum >= 50000 && sum < 299999) return 10;
                if (sum >= 300000) return 15;
                return 0;
            }
        }

        public override string ToString() => Title;
    }
}
