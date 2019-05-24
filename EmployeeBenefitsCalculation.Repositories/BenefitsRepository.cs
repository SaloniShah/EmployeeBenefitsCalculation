using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefitsCalculation.Repositories
{
    public class BenefitsRepository : IBenefitsRepository
    {
        public decimal GetYearlyBenefitsCostForEmployee()
        {
            //TODO: Get from database, possibly dependent on plan id
            return 1000;
        }
     
        public decimal GetYearlyBenefitsCostForSpouse()
        {
            //TODO: Get from database, possibly dependent on plan id
            return 500;
        }

        public decimal GetYearlyBenefitsCostForDependent()
        {
            //TODO: Get from database, possibly dependent on plan id
            return 500;
        }

    }
}
