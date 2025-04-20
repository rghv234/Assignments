using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayXpert.DAO;
using PayXpert.Entities;
using PayXpert.Exception;
using NUnit.Framework;

namespace PayXpert.Tests
{
    [TestFixture]
    public class PayXpertTests
    {
        private PayrollService _payrollService;

        [SetUp]
        public void Setup()
        {
            _payrollService = new PayrollService();
        }

        [Test]
        public void CalculateGrossSalaryForEmployee_ValidInput_ReturnsCorrectGrossSalary()
        {
            // Arrange
            int employeeId = 1;
            DateTime startDate = new DateTime(2025, 4, 1);
            DateTime endDate = new DateTime(2025, 4, 15);
            int expectedGrossSalary = 5500; // Changed to int to match schema (Basic 5000 + Overtime 500)

            // Act
            Payroll payroll = _payrollService.GeneratePayroll(employeeId, startDate, endDate);

            // Assert
            Assert.That(payroll.BasicSalary + payroll.OvertimePay, Is.EqualTo(expectedGrossSalary), "Gross salary calculation is incorrect");
        }

        [Test]
        public void CalculateNetSalaryAfterDeductions_ValidInput_ReturnsCorrectNetSalary()
        {
            // Arrange
            int employeeId = 1;
            DateTime startDate = new DateTime(2025, 4, 1);
            DateTime endDate = new DateTime(2025, 4, 15);
            int expectedNetSalary = 5300; // Changed to int to match schema (Gross 5500 - Deductions 200)

            // Act
            Payroll payroll = _payrollService.GeneratePayroll(employeeId, startDate, endDate);

            // Assert
            Assert.That(payroll.NetSalary, Is.EqualTo(expectedNetSalary), "Net salary after deductions is incorrect");
        }

        [Test]
        public void VerifyTaxCalculationForHighIncomeEmployee_ValidHighIncome_ReturnsCorrectTax()
        {
            // Arrange
            int employeeId = 1;
            int taxYear = 2025;
            int expectedTaxAmount = 12000; // Changed to int to match schema (20% of 60000)
            TaxService taxService = new TaxService();

            // Act
            Tax tax = taxService.CalculateTax(employeeId, taxYear);

            // Assert
            Assert.That(tax.TaxAmount, Is.EqualTo(expectedTaxAmount), "Tax calculation for high-income employee is incorrect");
        }

        [Test]
        public void ProcessPayrollForMultipleEmployees_ValidInput_ProcessesAllPayrolls()
        {
            // Arrange
            List<int> employeeIds = new List<int> { 1, 2, 3 };
            DateTime startDate = new DateTime(2025, 4, 1);
            DateTime endDate = new DateTime(2025, 4, 15);
            int expectedPayrollCount = employeeIds.Count;

            // Act
            List<Payroll> payrolls = new List<Payroll>();
            foreach (int id in employeeIds)
            {
                payrolls.Add(_payrollService.GeneratePayroll(id, startDate, endDate));
            }

            // Assert
            Assert.That(payrolls.Count, Is.EqualTo(expectedPayrollCount), "Payroll processing for multiple employees failed");
            Assert.That(payrolls.TrueForAll(p => p.NetSalary == 5300), Is.True, "Net salary calculation inconsistent across employees");
        }

        [Test]
        public void VerifyErrorHandlingForInvalidEmployeeData_InvalidEmployeeId_ThrowsException()
        {
            // Arrange
            int invalidEmployeeId = 999; // Non-existent employee
            DateTime startDate = new DateTime(2025, 4, 1);
            DateTime endDate = new DateTime(2025, 4, 15);

            // Act & Assert
            Assert.Throws<PayXpert.Exception.EmployeeNotFoundException>(() => _payrollService.GeneratePayroll(invalidEmployeeId, startDate, endDate), "Expected EmployeeNotFoundException for invalid employee ID was not thrown");
        }
    }
}
