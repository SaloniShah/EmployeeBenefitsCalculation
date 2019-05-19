using System;
using System.Globalization;
using EmployeeBenefitsCalculation.Objects;
using EmployeeBenefitsCalculation.Repositories;

namespace EmployeeBenefitsCalculation.Managers
{
    public class BenefitsCalculationManager : IBenefitsCalculationManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public BenefitsCalculationManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public BenefitsCost CalculateBenefitsCost(Employee employee)
        {
                  
            decimal yearlyBenefitCostForEmployee = 1000; //TODO: Configure values?
            decimal yearlyBenefitCostForSpouse = 500;
            decimal yearlyBenefitsCostForDependent = 500;
            int numberOfPayChecksPerYear =26;
            decimal otherDeductions = _employeeRepository.GetOtherDeducsiontsForEmployee(employee);
            decimal grossSalaryPerPayCheck = _employeeRepository.GetPerPaychheckGrossSalaryForEmployee(employee);       

            decimal discountedYearlyBenefitsCostForEmployee = getDiscountedPersonCost(yearlyBenefitCostForEmployee, employee);

            decimal totalYearlyCostForBenefits = discountedYearlyBenefitsCostForEmployee;

       
            if (employee.Spouse != null && !String.IsNullOrWhiteSpace(employee.Spouse?.Name))
            {
                totalYearlyCostForBenefits += getDiscountedPersonCost(yearlyBenefitCostForSpouse, employee.Spouse);
            }

            if (employee.Dependents != null && employee.Dependents?.Count > 0)
            {
                employee.Dependents.ForEach(d =>
                {
                    totalYearlyCostForBenefits += getDiscountedPersonCost(yearlyBenefitsCostForDependent, d);
                });
            }        

            decimal costPerPayCheckForEmployee = Math.Round(totalYearlyCostForBenefits / numberOfPayChecksPerYear, 2);
          
            var netSalaryPerPayCheck = grossSalaryPerPayCheck - costPerPayCheckForEmployee - otherDeductions;

            var costs = new BenefitsCost();
            costs.BenefitsCostPerPayCheck = costPerPayCheckForEmployee;
            costs.OtherDeductionsPerPayCheck = otherDeductions;
            costs.GrossSalaryPerPayCheck = grossSalaryPerPayCheck;
            costs.NetSalaryPerPayCheck = netSalaryPerPayCheck;
            costs.NumberOfPayChecksPerYear = numberOfPayChecksPerYear;
            return costs;
        }

        public decimal getDiscountedPersonCost(decimal yearlyCost, IPerson person)
        {
            //TODO: Configure discounts?
            var discountedCost = yearlyCost;

            if (person.Name.StartsWith("A", true, CultureInfo.CurrentCulture)) 
            {
                discountedCost = Math.Round(yearlyCost - (yearlyCost * 0.1m), 2);
            }

            return discountedCost;
        }   

    }
}
