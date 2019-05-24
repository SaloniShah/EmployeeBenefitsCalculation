using System;
using System.Collections.Generic;
using System.Text;
using EmployeeBenefitsCalculation.Objects;
using System.Globalization;

namespace EmployeeBenefitsCalculation.Managers.Discounts
{
    public class StartsWithADiscount : IDiscount
    {
        public decimal GetDiscountAmount(decimal yearlyCost, IPerson person)
        {
            var discountedCost = 0m;

            if (person.Name.StartsWith("A", true, CultureInfo.CurrentCulture))
            {
                discountedCost = yearlyCost * 0.1m;
            }

            return discountedCost;
        }
    }
}
