using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services
{
    public class RebateService : IRebateService
    {
        private readonly IRebateDataStore _rebateDataStore;
        private readonly IProductDataStore _productDataStore;
        private readonly Dictionary<IncentiveType, IIncentiveCalculator> _calculators;

        public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, Dictionary<IncentiveType, IIncentiveCalculator> calculators)
        {
            _rebateDataStore = rebateDataStore;
            _productDataStore = productDataStore;
            _calculators = calculators;
        }

        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {
            Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            Product product = _productDataStore.GetProduct(request.ProductIdentifier);

            var result = new CalculateRebateResult();

            if (_calculators.TryGetValue(rebate.Incentive, out var calculator) && calculator.Calculate(rebate, product, request, out var rebateAmount))
            {
                _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }
    }
}