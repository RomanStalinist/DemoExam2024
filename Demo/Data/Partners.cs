using System.Linq;

namespace Demo.Data
{
    public partial class Partner
    {
        public string TypeName => $"{Type} | {Title}";
        public byte Discount
        {
            get
            {
                var productCount = PartnerProducts.Sum(product => product.CountProduct);
                if (productCount < 9999)
                    return 0;
                if (productCount < 49999)
                    return 5;
                if (productCount < 299999)
                    return 10;
                return 15;
            }
        }
    }
}
