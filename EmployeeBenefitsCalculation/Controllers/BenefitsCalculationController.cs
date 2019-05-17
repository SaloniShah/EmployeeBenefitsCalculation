using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitsCalculation.Controllers
{
    [Route("api/[controller]")]
    public class BenefitsCalculationController : Controller
    {


        [HttpGet("[action]")]
        public double BenefitsCost()
        {
            return 3000;
        }

    }
}
