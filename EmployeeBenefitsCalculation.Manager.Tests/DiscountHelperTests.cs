using EmployeeBenefitsCalculation.Managers.Discounts;
using System;
using Xunit;

namespace EmployeeBenefitsCalculation.Manager.Tests
{
    public class DiscountHelperTests
    {
        private readonly DiscountHelper _discountHelper;

        public DiscountHelperTests()
        {
            _discountHelper = new DiscountHelper();
        }

        [Fact]
        public void Should_return_correct_list_of_discounts()
        {
            var result = _discountHelper.GetApplicableDiscounts();

            Assert.Single(result);
            Assert.Equal("EmployeeBenefitsCalculation.Managers.Discounts.StartsWithADiscount", result[0].GetType().ToString());
        }
    }
}
