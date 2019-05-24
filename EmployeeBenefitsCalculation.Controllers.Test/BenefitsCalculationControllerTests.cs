using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using EmployeeBenefitsCalculation.Managers;
using EmployeeBenefitsCalculation.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitsCalculation.Controllers.Test
{
    public class BenefitsCalculationControllerTests
    {
        private readonly BenefitsCalculationController _benefitsCalculationController;
        private readonly Mock<IBenefitsCalculationManager> _benefitsCalculationManager;

        public BenefitsCalculationControllerTests()
        {
            _benefitsCalculationManager = new Mock<IBenefitsCalculationManager>();
            _benefitsCalculationController = new BenefitsCalculationController(_benefitsCalculationManager.Object);
        }

        [Fact]
        public void Should_return_bad_request_when_employee_name_is_null() 
        {
            var employee = new Employee();
            employee.Name = null;

            var result =  _benefitsCalculationController.BenefitsCost(employee);
            var badResult = result as BadRequestResult;

            Assert.Equal(400, badResult.StatusCode);

        }

        [Fact]
        public void Should_return_bad_request_when_employee_name_is_empty_string()
        {
            var employee = new Employee();
            employee.Name = "";

            var result = _benefitsCalculationController.BenefitsCost(employee);
            var badResult = result as BadRequestResult;

            Assert.Equal(400, badResult.StatusCode);

        }

        [Fact]
        public void Should_return_bad_request_when_dependents_present_with_null_name()
        {
            var employee = new Employee();
            employee.Name = "Some name";
            var dependents = new List<Dependent>();
            var dependent = new Dependent();
            dependent.Name = null;
            dependents.Add(dependent);
            employee.Dependents = dependents;

            var result = _benefitsCalculationController.BenefitsCost(employee);
            var badResult = result as BadRequestResult;

            Assert.Equal(400, badResult.StatusCode);

        }

        [Fact]
        public void Should_return_bad_request_when_dependents_present_with_empty_string_name()
        {
            var employee = new Employee();
            employee.Name = "Some name";
            var dependents = new List<Dependent>();
            var dependent = new Dependent();
            dependent.Name = "";
            dependents.Add(dependent);
            employee.Dependents = dependents;

            var result = _benefitsCalculationController.BenefitsCost(employee);
            var badResult = result as BadRequestResult;

            Assert.Equal(400, badResult.StatusCode);

        }

        [Fact]
        public void Should_reutrn_correct_results_from_manager()
        {
            var employee = new Employee();
            employee.Name = "Some name";
            _benefitsCalculationManager.Setup(f => f.CalculateBenefitsCost(It.IsAny<Employee>())).Returns(GetBenefitsCost());

            var result = _benefitsCalculationController.BenefitsCost(employee);
            var okResult = result as OkObjectResult;
            var resultObject = okResult.Value as BenefitsCost;

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(2000, resultObject.GrossSalaryPerPayCheck);
            Assert.Equal(100, resultObject.BenefitsCostPerPayCheck);
            Assert.Equal(1900, resultObject.NetSalaryPerPayCheck);
            Assert.Equal(0, resultObject.OtherDeductionsPerPayCheck);
            Assert.Equal(26, resultObject.NumberOfPayChecksPerYear);
          
        }

        private BenefitsCost GetBenefitsCost()
        {
            var cost = new BenefitsCost();
            cost.GrossSalaryPerPayCheck = 2000;
            cost.BenefitsCostPerPayCheck = 100;
            cost.NetSalaryPerPayCheck = 1900;
            cost.OtherDeductionsPerPayCheck = 0;
            cost.NumberOfPayChecksPerYear = 26;
            return cost;
        }
    }
}
