using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefitsCalculation.Objects
{
    public class BenefitsCost
    {
        public decimal GrossSalaryPerPayCheck { get; set; }
        
        public decimal NetSalaryPerPayCheck { get; set; }

        public decimal BenefitsCostPerPayCheck { get; set; }
        
        public decimal OtherDeductionsPerPayCheck { get; set; }

        public int NumberOfPayChecksPerYear { get; set; }

    }
}
