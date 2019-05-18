using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeBenefitsCalculation.Objects;
using Microsoft.AspNetCore.Http;

namespace EmployeeBenefitsCalculation.Controllers
{
    [Route("api/[controller]")]
    public class BenefitsCalculationController : Controller
    {


        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BenefitsCost), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BenefitsCost([FromBody]Employee employee)
        {
            if (!IsEmployeeValid(employee))
            {
                return BadRequest();
            }

            var costs = new BenefitsCost();
            costs.BenefitsCostPerPayCheck = 200;
            costs.OtherDeductionsPerPayCheck = 0;
            costs.GrossSalaryPerPayCheck = 2000;
            costs.NetSalaryPerPayCheck = 1800;
            costs.NumberOfPayChecksPerYear = 26;
            return Ok(costs);
        }

        private Boolean IsEmployeeValid(Employee employee)
        {
            var isValid = true;

            if (employee.Name == null || employee.Name == "")
            {
                isValid = false;
            }

            if (employee.Dependents.Count > 0)
            {
                employee.Dependents.ForEach(d =>
                {
                    if (d.Name == null || d.Name == "")
                    {
                        isValid = false;
                    }
                });
            }
            return isValid;
        }

    }
}
