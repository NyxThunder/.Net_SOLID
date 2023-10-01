using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    public class ProductDataStore : IProductDataStore
    {
        public Product GetProduct(string productIdentifier)
        {
            // Implementation to get product from the data source
            // return new Product(); // Placeholder
            // Example product data
            return new Product
            {
                Identifier = productIdentifier,
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount,
                Price = 50
            };
        }
    }
}