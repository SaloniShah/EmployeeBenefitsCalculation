using EmployeeBenefitsCalculation.Objects;
using System;

namespace EmployeeBenefitsCalculation.Repositories
{
    public interface IEmployeeRepository
    {
        decimal GetPerPaychheckGrossSalaryForEmployee(Employee employee);

        decimal GetOtherDeducsiontsForEmployee(Employee employee);

    }
}
