using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IIncentiveCalculator
    {
        bool Calculate(Rebate rebate, Product product, CalculateRebateRequest request, out decimal rebateAmount);
    }
}