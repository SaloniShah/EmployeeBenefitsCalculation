using EmployeeBenefitsCalculation.Managers;
using EmployeeBenefitsCalculation.Objects;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmployeeBenefitsCalculation.Manager.Tests
{
    public class BenefitsCalculationManangerTests
    {
        private readonly IBenefitsCalculationManager _benefitsCalculationManager;

        public BenefitsCalculationManangerTests()
        {
            _benefitsCalculationManager = new BenefitsCalculationManager();
        }

        [Fact]
        public void Should_calculate_correct_cost_when_only_employee_present()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "Some Name";

            // Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(38.46m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1961.54m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }

        [Fact]
        public void Should_calculate_correct_cost_when_spouse_present()
        {
            //Arrange
            var employee = new Employee();
            employee.Name = "Some name";
            employee.Spouse = new Spouse();
            employee.Spouse.Name = "Other name";

            //Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(57.69m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1942.31m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);

        }
        [Fact]
        public void Should_calculate_correct_cost_when_a_dependent_present()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "Some name";
            employee.Dependents = new List<Dependent>();
            var dependent = new Dependent();
            dependent.Name = "Dependent name";
            employee.Dependents.Add(dependent);

            //Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(57.69m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1942.31m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);

        }

        [Fact]
        public void Should_calculate_correct_cost_when_2_dependents_present()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "Some name";
            employee.Dependents = new List<Dependent>();
            var dependent1 = new Dependent();
            dependent1.Name = "Dependent name";
            employee.Dependents.Add(dependent1);
            var dependent2 = new Dependent();
            dependent2.Name = "Dependent name 2";
            employee.Dependents.Add(dependent2);

            //Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(76.92m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1923.08m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        } 
        
    }
}
