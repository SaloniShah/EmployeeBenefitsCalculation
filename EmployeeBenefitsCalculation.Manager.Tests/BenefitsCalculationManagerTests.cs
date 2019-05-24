using EmployeeBenefitsCalculation.Managers;
using EmployeeBenefitsCalculation.Objects;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using EmployeeBenefitsCalculation.Repositories;
using EmployeeBenefitsCalculation.Managers.Discounts;

namespace EmployeeBenefitsCalculation.Manager.Tests
{
    public class BenefitsCalculationManangerTests
    {
        private readonly IBenefitsCalculationManager _benefitsCalculationManager;
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IBenefitsRepository> _benefitsRepository;
        private readonly Mock<IDiscountHelper> _discountHelper;

        public BenefitsCalculationManangerTests()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _benefitsRepository = new Mock<IBenefitsRepository>();
            _discountHelper = new Mock<IDiscountHelper>();

            _benefitsCalculationManager = new BenefitsCalculationManager(_employeeRepository.Object, _benefitsRepository.Object, _discountHelper.Object);
            _employeeRepository.Setup(f => f.GetPerPaychheckGrossSalaryForEmployee(It.IsAny<Employee>())).Returns(2000);
            _employeeRepository.Setup(f => f.GetOtherDeducsiontsForEmployee(It.IsAny<Employee>())).Returns(0);
            _benefitsRepository.Setup(f => f.GetYearlyBenefitsCostForEmployee()).Returns(1000);
            _benefitsRepository.Setup(f => f.GetYearlyBenefitsCostForSpouse()).Returns(500);
            _benefitsRepository.Setup(f => f.GetYearlyBenefitsCostForDependent()).Returns(500);
            _discountHelper.Setup(f => f.GetApplicableDiscounts()).Returns(GetApplicationDiscounts());

        }

       public List<IDiscount> GetApplicationDiscounts()
       {
            var discounts = new List<IDiscount>();
            discounts.Add(new StartsWithADiscount());
            return discounts;
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

            // Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(76.92m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1923.08m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        } 

        [Fact]
        public void Should_calculate_correct_discounted_cost_for_employee()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "A name";

            // Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(34.62m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1965.38m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }

        [Fact]
        public void Should_calculate_correct_discounted_cost_for_employee_ignoring_case()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "a name";

            // Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(34.62m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1965.38m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }

        [Fact]
        public void Should_calculate_correct_discounted_cost_for_spouse()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "Some name";
            employee.Spouse = new Spouse();
            employee.Spouse.Name = "An Other name";

            // Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(55.77m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1944.23m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }

        [Fact]
        public void Should_calculate_correct_discounted_cost_for_spouse_ignoring_case()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "Some name";
            employee.Spouse = new Spouse();
            employee.Spouse.Name = "an Other name";

            // Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(55.77m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1944.23m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }

        [Fact]
        public void Should_calculate_correct_discounted_cost_for_dependent()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "Some name";
            employee.Dependents = new List<Dependent>();
            var dependent = new Dependent();
            dependent.Name = "A dependent name";
            employee.Dependents.Add(dependent);

            //Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(55.77m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1944.23m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }

        [Fact]
        public void Should_calculate_correct_discounted_cost_for_dependent_ignoring_case()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "Some name";
            employee.Dependents = new List<Dependent>();
            var dependent = new Dependent();
            dependent.Name = "a dependent name";
            employee.Dependents.Add(dependent);

            //Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(55.77m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1944.23m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }

        [Fact]
        public void Should_calculate_correct_discounted_cost_for_employee_spouse_and_2_dependents()
        {
            // Arrange
            var employee = new Employee();
            employee.Name = "A name";
            employee.Spouse = new Spouse();
            employee.Spouse.Name = "A name 1";
            employee.Dependents = new List<Dependent>();
            var dependent1 = new Dependent();
            dependent1.Name = "A name 2";
            employee.Dependents.Add(dependent1);
            var dependent2 = new Dependent();
            dependent2.Name = "A name 3";
            employee.Dependents.Add(dependent2);

            // Act
            var result = _benefitsCalculationManager.CalculateBenefitsCost(employee);

            // Assert
            Assert.Equal(2000, result.GrossSalaryPerPayCheck);
            Assert.Equal(86.54m, result.BenefitsCostPerPayCheck);
            Assert.Equal(1913.46m, result.NetSalaryPerPayCheck);
            Assert.Equal(0, result.OtherDeductionsPerPayCheck);
            Assert.Equal(26, result.NumberOfPayChecksPerYear);
        }


    }
}
