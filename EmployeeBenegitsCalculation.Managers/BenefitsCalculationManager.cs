using System;
using EmployeeBenefitsCalculation.Objects;

namespace EmployeeBenefitsCalculation.Managers
{
    public class BenefitsCalculationManager : IBenefitsCalculationManager
    {
        public BenefitsCost CalculateBenefitsCost(Employee employee)
        {
            //TODO: Actual calculation
            var costs = new BenefitsCost();
            costs.BenefitsCostPerPayCheck = 200;
            costs.OtherDeductionsPerPayCheck = 0;
            costs.GrossSalaryPerPayCheck = 2000;
            costs.NetSalaryPerPayCheck = 1800;
            costs.NumberOfPayChecksPerYear = 26;
            return costs;
        }
    }
}
