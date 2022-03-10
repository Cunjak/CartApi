using AutoFixture;
using Business.HelperModels;
using Business.ServicesInterface;
using Cart.Controllers;
using Data.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CartTest.Controllers
{
    public class CartControllerTests
    {
        private readonly IFixture fixture;
        private readonly Mock<ICartService> serviceMock;
        private readonly CartController sut;

        public CartControllerTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            serviceMock = fixture.Freeze<Mock<ICartService>>();
            sut = new CartController(serviceMock.Object);
        }

        [Fact]
        public async Task Get_Cart_Overview_Should_Return_OkResponse_When_DataFound()
        {
            var cartOverviewMock = fixture.Create<CartOverview>();
            serviceMock.Setup(service => service.GetCartOverviewAsync()).ReturnsAsync(cartOverviewMock);

            var result = await sut.GetCartOverview();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<ObjectResult>().Value
                .Should()
                .NotBeNull().And
                .BeOfType(cartOverviewMock.GetType());

            serviceMock.Verify(service => service.GetCartOverviewAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_Cart_Overview_Should_Return_NotFound_Response_When_DataNotFound()
        {
            CartOverview response = null;
            serviceMock.Setup(service => service.GetCartOverviewAsync()).ReturnsAsync(response);

            var result = await sut.GetCartOverview();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();

            serviceMock.Verify(service => service.GetCartOverviewAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_CartItem_By_Id_Should_Return_OkResponse_When_Valid_Input()
        {
            string itemCartJsonMock = fixture.Create<string>();
            int id = fixture.Create<int>();
            serviceMock.Setup(service => service.GetCartItemByIdAsync(id)).ReturnsAsync(itemCartJsonMock);

            var result = await sut.GetCartItemById(id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull().And
                .BeOfType(itemCartJsonMock.GetType());

            serviceMock.Verify(service => service.GetCartItemByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task Get_CartItem_By_Id_Should_Return_BadRequest_When_Input_Is_Zero()
        {
            string itemCartJsonMock = fixture.Create<string>();
            int id = 0;
            serviceMock.Setup(service => service.GetCartItemByIdAsync(id)).ReturnsAsync(itemCartJsonMock);

            var result = await sut.GetCartItemById(id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();          

            serviceMock.Verify(service => service.GetCartItemByIdAsync(id), Times.Never);
        }

        [Fact]
        public async Task Get_CartItem_By_Id_Should_Return_BadRequest_When_Input_Is_Less_Than_Zero()
        {
            string itemCartJsonMock = fixture.Create<string>();
            int id = fixture.Create<int>();
            serviceMock.Setup(service => service.GetCartItemByIdAsync(-id)).ReturnsAsync(itemCartJsonMock);

            var result = await sut.GetCartItemById(-id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();

            serviceMock.Verify(service => service.GetCartItemByIdAsync(-id), Times.Never);
        }

        [Fact]
        public async Task Add_CartItem_Should_Return_CreatedAt_Response_When_Valid_Request()
        {
            var request = fixture.Create<CartItemFromBody>();
            var response = fixture.Create<CartItem>();
            serviceMock.Setup(service => service.AddCartItemAsync(request)).ReturnsAsync(response);

            var result = await sut.AddCartItem(request);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            result.As<CreatedAtActionResult>().Value
                .Should()
                .Be(JsonSerializer.Serialize(response));

            serviceMock.Verify(service => service.AddCartItemAsync(request), Times.Once);
        }

        [Fact]
        public async Task Add_CartItem_Should_Return_NotFound_Response_When_Cart_NotFound()
        {
            var request = fixture.Create<CartItemFromBody>();
            CartItem response = null;
            serviceMock.Setup(service => service.AddCartItemAsync(request)).ReturnsAsync(response);

            var result = await sut.AddCartItem(request);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<NotFoundResult>();
 
            serviceMock.Verify(service => service.AddCartItemAsync(request), Times.Once);
        }

        [Fact]
        public async Task Delete_CartItem_By_Id_Should_Return_OkResult_When_Valid_Input()
        {
            var id = fixture.Create<int>();
            serviceMock.Setup(service => service.DeleteCartItemAsync(id)).ReturnsAsync(true);

            var result = await sut.DeleteCartItemById(id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkResult>();          

            serviceMock.Verify(service => service.DeleteCartItemAsync(id), Times.Once);
        }

        [Fact]
        public async Task Delete_CartItem_By_Id_Should_Return_NotFound_When_Invalid_Input()
        {
            var id = fixture.Create<int>();
            serviceMock.Setup(service => service.DeleteCartItemAsync(id)).ReturnsAsync(false);

            var result = await sut.DeleteCartItemById(id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<NotFoundResult>();

            serviceMock.Verify(service => service.DeleteCartItemAsync(id), Times.Once);
        }

        [Fact]
        public async Task Delete_CartItem_By_Id_Should_Return_BadRequest_When_Input_Is_Zero()
        {
            var id = 0;
            serviceMock.Setup(service => service.DeleteCartItemAsync(id)).ReturnsAsync(true);

            var result = await sut.DeleteCartItemById(id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<BadRequestResult>();

            serviceMock.Verify(service => service.DeleteCartItemAsync(id), Times.Never);
        }

        [Fact]
        public async Task Cancel_Cart_Should_Return_OkResult_When_Data_Found()
        {
            
            serviceMock.Setup(service => service.CancelCartAsync()).ReturnsAsync(true);

            var result = await sut.CancelCart();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkResult>();

            serviceMock.Verify(service => service.CancelCartAsync(), Times.Once);
        }

        [Fact]
        public async Task Cancel_Cart_Should_Return_NotFoundResult_When_Data_Not_Found()
        {

            serviceMock.Setup(service => service.CancelCartAsync()).ReturnsAsync(false);

            var result = await sut.CancelCart();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<NotFoundResult>();

            serviceMock.Verify(service => service.CancelCartAsync(), Times.Once);
        }

        [Fact]
        public async Task Submit_Cart_Should_Return_OkResult_When_Data_Found()
        {

            serviceMock.Setup(service => service.SubmitCartAsync()).ReturnsAsync(true);

            var result = await sut.SubmitCart();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkResult>();

            serviceMock.Verify(service => service.SubmitCartAsync(), Times.Once);
        }

        [Fact]
        public async Task Submit_Cart_Should_Return_NotFoundResult_When_Data_Not_Found()
        {

            serviceMock.Setup(service => service.CancelCartAsync()).ReturnsAsync(false);

            var result = await sut.CancelCart();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<NotFoundResult>();

            serviceMock.Verify(service => service.CancelCartAsync(), Times.Once);
        }
    }
}
