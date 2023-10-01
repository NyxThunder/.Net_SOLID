using Smartwyre.DeveloperTest.Types;


namespace Smartwyre.DeveloperTest.Services
{
    public class FixedRateRebateCalculator : IIncentiveCalculator
    {
        public bool Calculate(Rebate rebate, Product product, CalculateRebateRequest request, out decimal rebateAmount)
        {
            rebateAmount = 0m;
            if (rebate == null || product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) || rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                return false;
            }
            rebateAmount = product.Price * rebate.Percentage * request.Volume;
            return true;
        }
    }
}