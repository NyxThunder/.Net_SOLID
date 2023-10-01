using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    public class RebateDataStore : IRebateDataStore
    {
        public Rebate GetRebate(string rebateIdentifier)
        {
            // Implementation to get rebate from the data source
            // return new Rebate(); // Placeholder

            // Example rebate data
            return new Rebate
            {
                Identifier = rebateIdentifier,
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100
            };
        }

        public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
        {
            // Implementation to store the rebate calculation result
        }
    }
}