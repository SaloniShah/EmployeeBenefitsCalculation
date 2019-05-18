using System;
using System.Collections.Generic;

namespace EmployeeBenefitsCalculation.Objects
{
    public class Employee
    {
        public string Name { get; set; } 

        public Spouse Spouse { get; set; }

        public List<Dependent> Dependents { get; set; }
    }
}
