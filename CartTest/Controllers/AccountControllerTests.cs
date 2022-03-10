
using AutoFixture;
using Business.HelperModels;
using Business.ServicesInterface;
using Cart.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CartTest.Controllers
{
    public class AccountControllerTests
    {
        private readonly IFixture fixture;
        private readonly Mock<IAccountService> serviceMock;
        private readonly AccountController sut;
        public AccountControllerTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            serviceMock = fixture.Freeze<Mock<IAccountService>>();
            sut = new AccountController(serviceMock.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOkResponse_WhenTokenStringGenerated()
        {
            var userMock = fixture.Create<User>();
            var token = fixture.Create<string>();
            serviceMock.Setup(service => service.LoginAsync(userMock)).ReturnsAsync(token);

            var result = await sut.Login(userMock);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<ObjectResult>().Value
                .Should()
                .Be(token);

            serviceMock.Verify(service => service.LoginAsync(userMock), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorizedResult_WhenTokenStringNull()
        {
            var userMock = fixture.Create<User>();
            string token = null;
            serviceMock.Setup(service => service.LoginAsync(userMock)).ReturnsAsync(token);

            var result = await sut.Login(userMock);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<UnauthorizedResult>();
            

            serviceMock.Verify(service => service.LoginAsync(userMock), Times.Once);
        }
    }
}
