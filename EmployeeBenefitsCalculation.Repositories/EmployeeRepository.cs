using System;
using System.Collections.Generic;
using System.Text;
using EmployeeBenefitsCalculation.Objects;

namespace EmployeeBenefitsCalculation.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public decimal GetPerPaychheckGrossSalaryForEmployee(Employee employee)
        {
            // TODO: Go to database and fetch salary
            return 2000;
        }

        public decimal GetOtherDeducsiontsForEmployee(Employee employee)
        {
            // TODO: Go to database and fetch other deductions for employee
            return 0;
        }
    }
}
