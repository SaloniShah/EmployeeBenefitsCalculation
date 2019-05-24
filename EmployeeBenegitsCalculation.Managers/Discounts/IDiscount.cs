using EmployeeBenefitsCalculation.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefitsCalculation.Managers.Discounts
{
    public interface IDiscount
    {
        decimal GetDiscountAmount(decimal yearlyCost, IPerson person);
    }
}
