using AutoFixture;
using Data.Constants;
using Data.Models;
using Data.RepositoryImplementation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DataTest.RepositoryImplementation
{
    public class CartRepositoryTests
    {
        private readonly IFixture fixture;
        private readonly DbContextOptions<CartDBContext> options;

        public CartRepositoryTests()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            options = new DbContextOptionsBuilder<CartDBContext>()
                        .UseInMemoryDatabase(databaseName: "CartDb")
                        .Options;

        }

        private CartDBContext GetDemoContext()
        {

            var context = new CartDBContext(options);

            var cart = new Cart() { Id = 1, CreatedBy = "Aleksandar", Status = "Draft" };

            context.Carts.Add(cart);

            var cartItems = new List<CartItem>()
            {
                new CartItem() {Id = 1, CreatedBy = "Aleksandar", Name = "Phone", Description = "Samsung"},
                new CartItem() {Id = 2, CreatedBy = "Aleksandar", Name = "Phone", Description = "Xiaomi"},
                new CartItem() {Id = 3, CreatedBy = "Aleksandar", Name = "Phone", Description = "Iphone"}
            };

            context.SaveChanges();

            context.CartItems.AddRange(cartItems);

            context.SaveChanges();

            var cartGet = context.Carts.FirstOrDefault();

            cart.CartItems = context.CartItems.ToList();

            context.CartItems.ForEachAsync(item => item.Cart = cart);

            context.SaveChanges();

            return context;          
        }

        [Fact]
        public async Task GetCartAsync_ShouldReturnData_WhenDataFound()
        {
            using (var context = GetDemoContext())
            {
                var cart = context.Carts.FirstOrDefault();
                var sut = new CartRepository(context);

                var result = await sut.GetCartAsync();

                result.Should().Be(cart);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task GetCartAsync_ShouldReturnNull_WhenDataNotFound()
        {
            using (var context = GetDemoContext())
            {
                var cart = context.Carts.FirstOrDefault();
                context.Remove(cart);
                context.SaveChanges();
                var sut = new CartRepository(context);

                var result = await sut.GetCartAsync();

                result.Should().BeNull();

                context.Database.EnsureDeleted();

            }
        }
        [Fact]
        public async Task GetCartItemByIdAsync_ShouldReturnData_WhenValidInput()
        {
            using (var context = GetDemoContext())
            {                
                var sut = new CartRepository(context);

                var result = await sut.GetCartItemByIdAsync(1);

                result.Should().BeAssignableTo<CartItem>();

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task GetCartItemByIdAsync_ShouldReturnNull_WhenInalidInput()
        {
            using (var context = GetDemoContext())
            {
                var sut = new CartRepository(context);

                var result = await sut.GetCartItemByIdAsync(6);

                result.Should().BeNull();

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task GetCartItemsAsync_ShouldReturnData_WhenDataFound()
        {
            using (var context = GetDemoContext())
            {
                var sut = new CartRepository(context);

                var result = await sut.GetCartItemsAsync();

                result.Should().BeAssignableTo<List<CartItem>>();

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task GetCartItemsAsync_ShouldReturnNull_WhenDataNotFound()
        {
            using (var context = GetDemoContext())
            {
                var sut = new CartRepository(context);
                var cartItems = context.CartItems.ToList();
                context.CartItems.RemoveRange(cartItems);
                context.CartItems = null;
                context.SaveChanges();

                var result = await sut.GetCartItemsAsync();

                result.Should().BeNull();

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task AddCartItemAsync_ShouldReturnAndAddData_WhenDataFound()
        {
            using (var context = GetDemoContext())
            {
                var cartItem = new CartItem() { Id = 4, CreatedBy = "Aleksandar", Name = "Phone", Description = "Lenovo" };
                var sut = new CartRepository(context);

                var result = await sut.AddCartItemAsync(cartItem);

                result.Should().Be(cartItem);

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task AddCartItemAsync_ShouldReturnNull_WhenDataNotFound()
        {
            using (var context = GetDemoContext())
            {
                var cartItem = new CartItem() { Id = 4, CreatedBy = "Aleksandar", Name = "Phone", Description = "Lenovo" };
                var cart = context.Carts.FirstOrDefault();
                context.Remove(cart);
                context.SaveChanges();
                var sut = new CartRepository(context);

                var result = await sut.AddCartItemAsync(cartItem);

                result.Should().BeNull();

                context.Database.EnsureDeleted();

            }
        }
        [Fact]
        public async Task DeleteCartItemAsync_ShouldReturnTrueAndADeleteData_WhenValidInput()
        {
            using (var context = GetDemoContext())
            {
                var sut = new CartRepository(context);

                var result = await sut.DeleteCartItemAsync(1);

                result.Should().Be(true);

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task DeleteCartItemAsync_ShouldReturnFalse_WhenInvalidInput()
        {
            using (var context = GetDemoContext())
            {
                var sut = new CartRepository(context);

                var result = await sut.DeleteCartItemAsync(4);

                result.Should().Be(false);

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task CancelCartAsync_ShouldReturnTrueAndCancelStatus_WhenDataFound()
        {
            using (var context = GetDemoContext())
            {
                var sut = new CartRepository(context);

                var result = await sut.CancelCartAsync();

                result.Should().Be(true);
                context.Carts.FirstOrDefault().Status.Should().Be(Status.Cancelled);

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task CancelCartAsync_ShouldReturnFalse_WhenDataNotFound()
        {
            using (var context = GetDemoContext())
            {
                var cart = context.Carts.FirstOrDefault();
                context.Remove(cart);
                context.SaveChanges();
                var sut = new CartRepository(context);

                var result = await sut.CancelCartAsync();

                result.Should().Be(false);

                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task SubmitCartAsync_ShouldReturnTrueAndSubmit_WhenDataFound()
        {
            using (var context = GetDemoContext())
            {               
                var sut = new CartRepository(context);

                var result = await sut.SubmitCartAsync();

                result.Should().Be(true);
                context.Carts.FirstOrDefault().Status.Should().Be(Status.Submitted);


                context.Database.EnsureDeleted();

            }
        }

        [Fact]
        public async Task SubmitCartAsync_ShouldReturnFalse_WhenDataNotFound()
        {
            using (var context = GetDemoContext())
            {
                var cart = context.Carts.FirstOrDefault();
                context.Remove(cart);
                context.SaveChanges();
                var sut = new CartRepository(context);

                var result = await sut.SubmitCartAsync();

                result.Should().Be(false);

                context.Database.EnsureDeleted();

            }
        }
    }
}
