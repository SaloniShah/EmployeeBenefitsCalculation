using EmployeeBenefitsCalculation.Managers.Discounts;
using EmployeeBenefitsCalculation.Objects;
using System;
using Xunit;

namespace EmployeeBenefitsCalculation.Manager.Tests
{
    public class StartsWithADiscountTests
    {
        private readonly StartsWithADiscount _discount;

        public StartsWithADiscountTests()
        {
            _discount = new StartsWithADiscount();
        }

        [Fact]
        public void Should_return_correct_discount_when_name_starts_with_A()
        {
            IPerson person = new Employee();
            person.Name = "A name";

            var result = _discount.GetDiscountAmount(1000, person);

            Assert.Equal(100m, result);
        }

        [Fact]
        public void Should_return_correct_discount_when_name_starts_with_a_ignoring_case()
        {
            IPerson person = new Employee();
            person.Name = "a name";

            var result = _discount.GetDiscountAmount(1000, person);

            Assert.Equal(100m, result);
        }

        [Fact]
        public void Should_return_correct_discount_when_name_does_not_start_with_A()
        {
            IPerson person = new Employee();
            person.Name = "B name";

            var result = _discount.GetDiscountAmount(1000, person);

            Assert.Equal(0m, result);
        }

    }
}
