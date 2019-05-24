using System;
using System.Globalization;
using EmployeeBenefitsCalculation.Managers.Discounts;
using EmployeeBenefitsCalculation.Objects;
using EmployeeBenefitsCalculation.Repositories;

namespace EmployeeBenefitsCalculation.Managers
{
    public class BenefitsCalculationManager : IBenefitsCalculationManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBenefitsRepository _benefitsRepository;
        private readonly IDiscountHelper _discountHelper;
        
        public BenefitsCalculationManager(IEmployeeRepository employeeRepository,
                                           IBenefitsRepository benefitsRepository,
                                           IDiscountHelper discountHelper)
        {
            _employeeRepository = employeeRepository;
            _benefitsRepository = benefitsRepository;
            _discountHelper = discountHelper;
        }

        public BenefitsCost CalculateBenefitsCost(Employee employee)
        {
                  
            decimal yearlyBenefitCostForEmployee = _benefitsRepository.GetYearlyBenefitsCostForEmployee();
            decimal yearlyBenefitCostForSpouse = _benefitsRepository.GetYearlyBenefitsCostForSpouse();
            decimal yearlyBenefitsCostForDependent = _benefitsRepository.GetYearlyBenefitsCostForDependent();
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
            var discountedCost = yearlyCost;

            var discounts = _discountHelper.GetApplicableDiscounts();

            discounts.ForEach(d =>
            {
                discountedCost = discountedCost - d.GetDiscountAmount(yearlyCost, person);
            });         

            return discountedCost;
        }   

    }
}
