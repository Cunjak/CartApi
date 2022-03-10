using AutoFixture;
using Data.RepositoryImplementation;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DataTest.RepositoryImplementation
{
    public class AccountRepositoryTests
    {
        private readonly IFixture fixture;
        private readonly Mock<UserManager<IdentityUser>> userManagerMock;
        private readonly AccountRepository sut;

        public AccountRepositoryTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            sut = new AccountRepository(userManagerMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnUserRoles_WhenUserFound()
        {
            var username = fixture.Create<string>();
            var password = fixture.Create<string>();
            var userRolesMock = fixture.Create<IList<string>>();
            var user = fixture.Create<IdentityUser>();
            userManagerMock.Setup(manager => manager.FindByNameAsync(username)).ReturnsAsync(user);
            userManagerMock.Setup(manager => manager.CheckPasswordAsync(user, password)).ReturnsAsync(true);
            userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(userRolesMock);

            var result = await sut.LoginAsync(username, password);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IList<string>>();

            userManagerMock.Verify(manager => manager.FindByNameAsync(username), Times.Once);
            userManagerMock.Verify(manager => manager.CheckPasswordAsync(user, password), Times.Once);
            userManagerMock.Verify(manager => manager.GetRolesAsync(user), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserNotFound()
        {
            var username = fixture.Create<string>();
            var password = fixture.Create<string>();
            var userRolesMock = fixture.Create<IList<string>>();
            IdentityUser user = null;
            userManagerMock.Setup(manager => manager.FindByNameAsync(username)).ReturnsAsync(user);
            userManagerMock.Setup(manager => manager.CheckPasswordAsync(user, password)).ReturnsAsync(true);
            userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(userRolesMock);

            var result = await sut.LoginAsync(username, password);

            result.Should().BeNull();

            userManagerMock.Verify(manager => manager.FindByNameAsync(username), Times.Once);
            userManagerMock.Verify(manager => manager.CheckPasswordAsync(user, password), Times.Never);
            userManagerMock.Verify(manager => manager.GetRolesAsync(user), Times.Never);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserAndPasswordNotFound()
        {
            var username = fixture.Create<string>();
            var password = fixture.Create<string>();
            var userRolesMock = fixture.Create<IList<string>>();
            IdentityUser user = null;
            userManagerMock.Setup(manager => manager.FindByNameAsync(username)).ReturnsAsync(user);
            userManagerMock.Setup(manager => manager.CheckPasswordAsync(user, password)).ReturnsAsync(false);
            userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(userRolesMock);

            var result = await sut.LoginAsync(username, password);

            result.Should().BeNull();

            userManagerMock.Verify(manager => manager.FindByNameAsync(username), Times.Once);
            userManagerMock.Verify(manager => manager.CheckPasswordAsync(user, password), Times.Never);
            userManagerMock.Verify(manager => manager.GetRolesAsync(user), Times.Never);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordNotFound()
        {
            var username = fixture.Create<string>();
            var password = fixture.Create<string>();
            var userRolesMock = fixture.Create<IList<string>>();
            var user = fixture.Create<IdentityUser>();
            userManagerMock.Setup(manager => manager.FindByNameAsync(username)).ReturnsAsync(user);
            userManagerMock.Setup(manager => manager.CheckPasswordAsync(user, password)).ReturnsAsync(false);
            userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(userRolesMock);

            var result = await sut.LoginAsync(username, password);

            result.Should().BeNull();

            userManagerMock.Verify(manager => manager.FindByNameAsync(username), Times.Once);
            userManagerMock.Verify(manager => manager.CheckPasswordAsync(user, password), Times.Once);
            userManagerMock.Verify(manager => manager.GetRolesAsync(user), Times.Never);
        }
    }
}
