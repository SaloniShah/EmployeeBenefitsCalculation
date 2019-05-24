using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefitsCalculation.Repositories
{
    public interface IBenefitsRepository
    {
        decimal GetYearlyBenefitsCostForEmployee();

        decimal GetYearlyBenefitsCostForSpouse();

        decimal GetYearlyBenefitsCostForDependent();

    }
}
