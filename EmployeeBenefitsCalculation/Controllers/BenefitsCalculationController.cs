using System;
using Microsoft.AspNetCore.Mvc;
using EmployeeBenefitsCalculation.Objects;
using Microsoft.AspNetCore.Http;
using EmployeeBenefitsCalculation.Managers;

namespace EmployeeBenefitsCalculation.Controllers
{
    [Route("api/[controller]")]
    public class BenefitsCalculationController : Controller
    {

        private readonly IBenefitsCalculationManager _benefitsCalculationManager;

        public BenefitsCalculationController(IBenefitsCalculationManager benefitsCalculationManager)
        {
            _benefitsCalculationManager = benefitsCalculationManager;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BenefitsCost), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BenefitsCost([FromBody]Employee employee)
        {
            if (!IsEmployeeValid(employee))
            {
                return BadRequest();
            }

            var costs =  _benefitsCalculationManager.CalculateBenefitsCost(employee);
            return Ok(costs);   
        }

        private Boolean IsEmployeeValid(Employee employee)
        {
            var isValid = true;

            if (employee.Name == null || employee.Name == "")
            {
                isValid = false;
            }

            if (employee.Dependents?.Count > 0)
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
