using System;
using System.Collections.Generic;
using System.Text;
using EmployeeBenefitsCalculation.Objects;

namespace EmployeeBenefitsCalculation.Managers
{
    public interface IBenefitsCalculationManager
    {
        BenefitsCost CalculateBenefitsCost(Employee employee);
    }
}
