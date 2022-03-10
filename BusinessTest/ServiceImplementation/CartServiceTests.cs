using AutoFixture;
using AutoMapper;
using Business.HelperModels;
using Business.ServiceImplementation;
using Data.Models;
using Data.RepositoryInterface;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BusinessTest.ServiceImplementation
{
    public class CartServiceTests
    {
        private readonly IFixture fixture;
        private readonly Mock<ICartRepository> repositoryMock;
        private readonly Mock<ILogger<CartService>> loggerMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly CartService sut;

        public CartServiceTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            repositoryMock = fixture.Freeze<Mock<ICartRepository>>();
            loggerMock = fixture.Freeze<Mock<ILogger<CartService>>>();
            mapperMock = new Mock<IMapper>();

            sut = new CartService(mapperMock.Object, repositoryMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task Get_Cart_Overview_Should_Return_Data_When_DataFound()
        {
            var cartOverviewMock = fixture.Create<CartOverview>();
            var cartMock = fixture.Create<Cart>();
            var cartItemsMock = fixture.Create<List<CartItem>>();
            repositoryMock.Setup(rep => rep.GetCartAsync()).ReturnsAsync(cartMock);
            repositoryMock.Setup(rep => rep.GetCartItemsAsync()).ReturnsAsync(cartItemsMock);
            cartMock.CartItems = cartItemsMock;
            mapperMock.Setup(mapper => mapper.Map<CartOverview>(cartMock)).Returns(cartOverviewMock);
            mapperMock.Setup(mapper => mapper.Map<List<CartItemBasicAttributes>>(cartMock.CartItems)).Returns(cartOverviewMock.CartItemsBasicAttributes);

            var result = await sut.GetCartOverviewAsync();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CartOverview>();

            repositoryMock.Verify(service => service.GetCartAsync(), Times.Once);
            repositoryMock.Verify(service => service.GetCartItemsAsync(), Times.Once);

        }

        [Fact]
        public async Task Get_Cart_Overview_Should_Return_Null_When_DataNotFound()
        {
            Cart cartMock = null;
            repositoryMock.Setup(rep => rep.GetCartAsync()).ReturnsAsync(cartMock);
          
            var result = await sut.GetCartOverviewAsync();

            result.Should().BeNull();

            repositoryMock.Verify(service => service.GetCartAsync(), Times.Once);
            repositoryMock.Verify(service => service.GetCartItemsAsync(), Times.Never);

        }

        [Fact]
        public async Task Get_CartItem_By_Id_Should_Return_Data_When_Data_Found()
        {
            CartItem cartItem = fixture.Create<CartItem>();
            string cartItemJson = JsonSerializer.Serialize(cartItem);
            int id = fixture.Create<int>();
            repositoryMock.Setup(rep => rep.GetCartItemByIdAsync(id)).ReturnsAsync(cartItem);

            var result = await sut.GetCartItemByIdAsync(id);

            result.Should().NotBeNull();
            result.Should().Be(cartItemJson);

            repositoryMock.Verify(service => service.GetCartItemByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task Get_CartItem_By_Id_Should_Return_Null_When_Data_NotFound()
        {
            CartItem cartItem = null;
            int id = fixture.Create<int>();
            repositoryMock.Setup(rep => rep.GetCartItemByIdAsync(id)).ReturnsAsync(cartItem);

            var result = await sut.GetCartItemByIdAsync(id);

            result.Should().BeNull();

            repositoryMock.Verify(service => service.GetCartItemByIdAsync(id), Times.Once);
            loggerMock.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task AddCartItemAsync_ShouldReturnData_WhenDataFound()
        {
            var cartItemFromBody = fixture.Create<CartItemFromBody>();
            var cartItem = fixture.Create<CartItem>();
            mapperMock.Setup(mapp => mapp.Map<CartItem>(cartItemFromBody)).Returns(cartItem);
            repositoryMock.Setup(rep => rep.AddCartItemAsync(cartItem)).ReturnsAsync(cartItem);

            var result = await sut.AddCartItemAsync(cartItemFromBody);

            result.Should().NotBeNull();
            result.Should().Be(cartItem);

            repositoryMock.Verify(service => service.AddCartItemAsync(cartItem), Times.Once);
            mapperMock.Verify(mapp => mapp.Map<CartItem>(cartItemFromBody), Times.Once);
        }

        [Fact]
        public async Task AddCartItemAsync_ShouldReturnNull_WhenDataNotFound()
        {
            var cartItemFromBody = fixture.Create<CartItemFromBody>();
            var cartItem = fixture.Create<CartItem>();
            CartItem carItemResponse = null;
            mapperMock.Setup(mapp => mapp.Map<CartItem>(cartItemFromBody)).Returns(cartItem);
            repositoryMock.Setup(rep => rep.AddCartItemAsync(cartItem)).ReturnsAsync(carItemResponse);

            var result = await sut.AddCartItemAsync(cartItemFromBody);

            result.Should().BeNull();

            repositoryMock.Verify(service => service.AddCartItemAsync(cartItem), Times.Once);
            mapperMock.Verify(mapp => mapp.Map<CartItem>(cartItemFromBody), Times.Once);
            loggerMock.Verify(x => x.Log(
               LogLevel.Error,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCartItemAsync_ShouldReturnTrue_WhenDataFound()
        {
            var id = fixture.Create<int>();
            repositoryMock.Setup(rep => rep.DeleteCartItemAsync(id)).ReturnsAsync(true);

            var result = await sut.DeleteCartItemAsync(id);

            result.Should().Be(true);

            repositoryMock.Verify(service => service.DeleteCartItemAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteCartItemAsync_ShouldReturnFalse_WhenDataNotFound()
        {
            var id = fixture.Create<int>();
            repositoryMock.Setup(rep => rep.DeleteCartItemAsync(id)).ReturnsAsync(false);

            var result = await sut.DeleteCartItemAsync(id);

            result.Should().Be(false);

            repositoryMock.Verify(service => service.DeleteCartItemAsync(id), Times.Once);

        }

        [Fact]
        public async Task CancelCartAsync_ShouldReturnTrue_WhenCartCancelled()
        {
            repositoryMock.Setup(rep => rep.CancelCartAsync()).ReturnsAsync(true);

            var result = await sut.CancelCartAsync();

            result.Should().Be(true);

            repositoryMock.Verify(service => service.CancelCartAsync(), Times.Once);
        }

        [Fact]
        public async Task CancelCartAsync_ShouldReturnFalse_WhenCartNotCancelled()
        {
            repositoryMock.Setup(rep => rep.CancelCartAsync()).ReturnsAsync(false);

            var result = await sut.CancelCartAsync();

            result.Should().Be(false);
            
            repositoryMock.Verify(service => service.CancelCartAsync(), Times.Once);
            loggerMock.Verify(x => x.Log(
              LogLevel.Error,
              It.IsAny<EventId>(),
              It.IsAny<It.IsAnyType>(),
              It.IsAny<Exception>(),
              (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task SubmitCartAsync_ShouldReturnTrue_WhenCartSubmitted()
        {
            repositoryMock.Setup(rep => rep.SubmitCartAsync()).ReturnsAsync(true);
            sut.SubmitToOtherSystem();

            var result = await sut.SubmitCartAsync();

            result.Should().Be(true);

            repositoryMock.Verify(service => service.SubmitCartAsync(), Times.Once);
        }

        [Fact]
        public async Task SubmitCartAsync_ShouldReturnFalse_WhenCartNotSubmitted()
        {
            repositoryMock.Setup(rep => rep.SubmitCartAsync()).ReturnsAsync(false);

            var result = await sut.SubmitCartAsync();

            result.Should().Be(false);

            repositoryMock.Verify(service => service.SubmitCartAsync(), Times.Once);
        }

        [Fact]
        public void SubmitToOtherSystem_ShouldDoNothing()
        {
            sut.SubmitToOtherSystem();

            repositoryMock.VerifyAll();
            loggerMock.VerifyAll();
            mapperMock.VerifyAll();
        }
    }
}
