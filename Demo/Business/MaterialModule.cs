using System;
using System.Linq;

namespace Demo.Business
{
    public static class MaterialModule
    {
        public static int CalculateMaterialCount(
            int productTypeId,
            int materialTypeId,
            int productsCount,
            double length,
            double width)
        {
            var productType = App.Context.ProductTypes.SingleOrDefault(pt => pt.ID == productTypeId);
            var materialType = App.Context.MaterialTypes.SingleOrDefault(mt => mt.ID == materialTypeId);

            if (productType is null || materialType is null || productsCount <= 0 || length <= 0 || width <= 0)
                return -1;

            var productTypeCoefficient = (double)productType.Ratio;
            var scrapRate = (double)materialType.ScrapRate;

            if (productTypeCoefficient <= 0 || scrapRate <= 0)
                return -1;

            return (int)Math.Ceiling(length * width * productTypeCoefficient * (1 + scrapRate) * productsCount);
        }
    }
}
