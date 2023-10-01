using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public class AmountPerUomCalculator : IIncentiveCalculator
    {
        public bool Calculate(Rebate rebate, Product product, CalculateRebateRequest request, out decimal rebateAmount)
        {
            rebateAmount = 0m;
            if (rebate == null || product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) || rebate.Amount == 0 || request.Volume == 0)
            {
                return false;
            }
            rebateAmount = rebate.Amount * request.Volume;
            return true;
        }
    }
}