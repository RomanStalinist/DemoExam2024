using System.Linq;

namespace Demo1.Data
{
    public partial class PartnerProduct
    {
        public string TitleProduct => App.Context.Products.SingleOrDefault(p => p.ID == ProductID)?.Title ?? "Название не указано";
    }
}
