using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefitsCalculation.Managers.Discounts
{
    public interface IDiscountHelper
    {
        List<IDiscount> GetApplicableDiscounts();
    }
}
