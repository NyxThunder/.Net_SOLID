using System;
using Xunit;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateServiceTests
    {
        private readonly Mock<IRebateDataStore> _rebateDataStoreMock;
        private readonly Mock<IProductDataStore> _productDataStoreMock;
        private readonly Dictionary<IncentiveType, IIncentiveCalculator> _calculators;
        private readonly RebateService _rebateService;

        public RebateServiceTests()
        {
            _rebateDataStoreMock = new Mock<IRebateDataStore>();
            _productDataStoreMock = new Mock<IProductDataStore>();
            _calculators = new Dictionary<IncentiveType, IIncentiveCalculator>
            {
                { IncentiveType.FixedCashAmount, new FixedCashAmountCalculator() },
                { IncentiveType.FixedRateRebate, new FixedRateRebateCalculator() },
                { IncentiveType.AmountPerUom, new AmountPerUomCalculator() }
            };
            _rebateService = new RebateService(_rebateDataStoreMock.Object, _productDataStoreMock.Object, _calculators);
        }

        [Fact]
        public void Calculate_ShouldReturnSuccess_ForFixedCashAmount()
        {
            // Arrange
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate1", ProductIdentifier = "product1", Volume = 10 };
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            _rebateDataStoreMock.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(rebate);
            _productDataStoreMock.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(product);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturnSuccess_ForFixedRateRebate()
        {
            // Arrange
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate2", ProductIdentifier = "product2", Volume = 10 };
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 50 };

            _rebateDataStoreMock.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(rebate);
            _productDataStoreMock.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(product);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturnSuccess_ForAmountPerUom()
        {
            // Arrange
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate3", ProductIdentifier = "product3", Volume = 10 };
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 5 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            _rebateDataStoreMock.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(rebate);
            _productDataStoreMock.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(product);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturnFailure_ForUnsupportedIncentive()
        {
            // Arrange
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate4", ProductIdentifier = "product4", Volume = 10 };
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

            _rebateDataStoreMock.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(rebate);
            _productDataStoreMock.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(product);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }
    }
}