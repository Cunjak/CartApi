using System;
using Xunit;
using Moq;
using FluentAssertions;
using AutoFixture;
using Business.ServicesInterface;
using System.Threading.Tasks;
using Business.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using System.Text.Json;
using Data.RepositoryInterface;
using Business.ServiceImplementation;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Business.Helper;
using System.Collections.Generic;
using Business.Constants;
using Microsoft.Extensions.Configuration;

namespace BusinessTest.ServiceImplementation
{
    public class AccountServiceTests
    {

        private readonly IFixture fixture;
        private readonly Mock<IAccountRepository> repositoryMock;
        private readonly Mock<IConfiguration> configurationMock;
        private readonly AccountService sut;

        public AccountServiceTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            repositoryMock = fixture.Freeze<Mock<IAccountRepository>>();
            configurationMock = fixture.Freeze<Mock<IConfiguration>>();

            sut = new AccountService(configurationMock.Object, repositoryMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenUserFound()
        {
            var userMock = fixture.Create<User>();
            var userRolesMock = fixture.Create<IList<string>>();
            string jwtSecret = fixture.Create<string>();
            repositoryMock.Setup(rep => rep.LoginAsync(userMock.Username, userMock.Password)).ReturnsAsync(userRolesMock);
            configurationMock.Setup(config => config["JWT:Secret"]).Returns(jwtSecret);

            var result = await sut.LoginAsync(userMock);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<string>();

            repositoryMock.Verify(service => service.LoginAsync(userMock.Username, userMock.Password), Times.Once);

        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserRolesNotFound()
        {
            var userMock = fixture.Create<User>();
            IList<string> userRolesMock = null;
            string jwtSecret = fixture.Create<string>();
            repositoryMock.Setup(rep => rep.LoginAsync(userMock.Username, userMock.Password)).ReturnsAsync(userRolesMock);
            configurationMock.Setup(config => config["JWT:Secret"]).Returns(jwtSecret);

            var result = await sut.LoginAsync(userMock);

            result.Should().BeNull();

            repositoryMock.Verify(service => service.LoginAsync(userMock.Username, userMock.Password), Times.Once);

        }
    }
}
