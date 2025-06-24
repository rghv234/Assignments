using EasypayBackend.Controllers;
using EasypayBackend.Dtos;
using EasypayBackend.Models;
using EasypayBackend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasypayBackend.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private EmployeeController _controller;

        [SetUp]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _controller = new EmployeeController(_employeeRepositoryMock.Object);
        }

        [Test]
        public async Task GetEmployee_ExistingId_ReturnsOk()
        {
            var employee = new Employee { Id = 1, UserId = 1, Name = "John Doe" };
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Employee")
            }));
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };

            var result = await _controller.GetEmployee(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var employeeDto = result.Value as EmployeeDto;
            Assert.AreEqual(employee.Id, employeeDto.Id);
        }

        [Test]
        public async Task CreateEmployee_ValidDto_ReturnsOk()
        {
            var employeeDto = new EmployeeDto { UserId = 1, Name = "John Doe", Salary = 50000 };
            var employee = new Employee { Id = 1, UserId = 1, Name = "John Doe", Salary = 50000 };
            _employeeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);
            _employeeRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            }));
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };

            var result = await _controller.CreateEmployee(employeeDto) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var returnedDto = result.Value as EmployeeDto;
            Assert.AreEqual(employeeDto.Name, returnedDto.Name);
        }
    }
}