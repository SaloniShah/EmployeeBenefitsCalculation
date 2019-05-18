using System;
using EmployeeBenefitsCalculation.Objects;

namespace EmployeeBenefitsCalculation.Managers
{
    public class BenefitsCalculationManager : IBenefitsCalculationManager
    {
        public BenefitsCost CalculateBenefitsCost(Employee employee)
        {
                  
            decimal yearlyBenefitCostForEmployee = 1000; //TODO: Configure
            decimal yearlyBenefitCostForSpouse = 500; //TODO: COnfigure
            decimal yearlyBenefitsCostForDependent = 500; //TODO: Configure

            decimal totalYearlyCostForBenefits = yearlyBenefitCostForEmployee;

            //TODO: Apply discounts

            if (employee.Spouse != null && !String.IsNullOrWhiteSpace(employee.Spouse?.Name))
            {
                totalYearlyCostForBenefits += yearlyBenefitCostForSpouse;
            }

            if (employee.Dependents != null && employee.Dependents?.Count > 0)
            {
                employee.Dependents.ForEach(f =>
                {
                    totalYearlyCostForBenefits += yearlyBenefitsCostForDependent;
                });
            }

            int numberOfPayChecksPerYear = 26; //TODO: Configure


            decimal costPerPayCheckForEmployee = Math.Round(totalYearlyCostForBenefits / numberOfPayChecksPerYear, 2);
            var otherDeductions = 0;
            decimal grossSalaryPerPayCheck = 2000;
            var netSalaryPerPayCheck = grossSalaryPerPayCheck - costPerPayCheckForEmployee - otherDeductions;

            var costs = new BenefitsCost();
            costs.BenefitsCostPerPayCheck = costPerPayCheckForEmployee;
            costs.OtherDeductionsPerPayCheck = otherDeductions;
            costs.GrossSalaryPerPayCheck = grossSalaryPerPayCheck;
            costs.NetSalaryPerPayCheck = netSalaryPerPayCheck;
            costs.NumberOfPayChecksPerYear = numberOfPayChecksPerYear;
            return costs;
        }
    }
}
