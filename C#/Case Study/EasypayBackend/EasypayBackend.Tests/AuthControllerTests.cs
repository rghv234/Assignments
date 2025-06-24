using EasypayBackend.Controllers;
using EasypayBackend.Dtos;
using EasypayBackend.Models;
using EasypayBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EasypayBackend.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<ILogger<AuthController>> _loggerMock;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private AuthController _controller;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<AuthController>>(); // Add logger mock
            _employeeRepositoryMock = new Mock<IEmployeeRepository>(); // Add employee repository mock

            _controller = new AuthController(
                _userRepositoryMock.Object,
                _configurationMock.Object,
                _loggerMock.Object, // Provide logger
                _employeeRepositoryMock.Object); // Provide employee repository
        }

        [Test]
        public async Task Register_ValidInput_ReturnsOk()
        {
            var registerDto = new RegisterWithEmployeeDto
            {
                Email = "test@easypay.com",
                Password = "Test@123",
                Role = "Employee",
                Name = "Test User"
            };
            _userRepositoryMock.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
                              .ReturnsAsync(new List<User>());
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _employeeRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);
            _userRepositoryMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _controller.Register(registerDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            var returnValue = okResult.Value as dynamic;
            Assert.AreEqual("test@easypay.com", returnValue.Email);
        }

        [Test]
        public async Task Register_EmailExists_ReturnsBadRequest()
        {
            var registerDto = new RegisterWithEmployeeDto
            {
                Email = "test@easypay.com",
                Password = "Test@123",
                Role = "Employee",
                Name = "Test User"
            };
            _userRepositoryMock.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
                              .ReturnsAsync(new List<User> { new User { Email = "test@easypay.com" } });

            var result = await _controller.Register(registerDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(400, badRequestResult.StatusCode);
            var returnValue = badRequestResult.Value as dynamic;
            Assert.AreEqual("User already exists", returnValue.Message);
        }

        // Add more tests as needed (e.g., Login)
    }
}