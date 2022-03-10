using AutoMapper;
using Business.Constants;
using Business.HelperModels;
using Business.ServicesInterface;
using Data.Models;
using Data.RepositoryInterface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.ServiceImplementation
{
    public class CartService: ICartService
    {
        private readonly IMapper mapper;
        private readonly ICartRepository repository;
        private readonly ILogger<CartService> logger;

        public CartService(IMapper mapper, ICartRepository repository, ILogger<CartService> logger)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.logger = logger;
        }
        public async Task<CartOverview> GetCartOverviewAsync()
        {
            var cart = await repository.GetCartAsync();

            if (cart == null)

                return null;

            cart.CartItems = await repository.GetCartItemsAsync();

            var cartOverview = mapper.Map<CartOverview>(cart);

            cartOverview.CartItemsBasicAttributes = mapper.Map<List<CartItemBasicAttributes>>(cart.CartItems);

            return cartOverview;
        }
        public async Task<string> GetCartItemByIdAsync(int cartItemId)
        {
            var cartItem = await repository.GetCartItemByIdAsync(cartItemId);

            if(cartItem == null)
            {
                logger.LogError("error from GetCartItemByIdAsync {0}", LogMessage.itemNotInDatabase);

                return null;
            }         

            return JsonSerializer.Serialize(cartItem);
        }
        public async Task<CartItem> AddCartItemAsync(CartItemFromBody cartItemFromBody)
        {
            var cartItem = mapper.Map<CartItem>(cartItemFromBody);

            var cartItemFromDb = await repository.AddCartItemAsync(cartItem);

            if(cartItemFromDb == null)
            
                logger.LogError("error from AddCartItem, {0}", LogMessage.cartNotInDatabase);

            return cartItemFromDb;
        }
        public async Task<bool> DeleteCartItemAsync(int cartItemId)
        {

            var isDeleted = await repository.DeleteCartItemAsync(cartItemId);

            if (isDeleted == false)
            
                logger.LogError("error from Cart controller, CancelCart action, {0}", LogMessage.itemNotInDatabase);           

            return isDeleted;
        }
        public async Task<bool> CancelCartAsync()
        {
            var isCancelled = await repository.CancelCartAsync();

            if(isCancelled == false)

                logger.LogError("error from AddCartItem, {0}", LogMessage.cartNotInDatabase);

            return isCancelled;
        }
        public async Task<bool> SubmitCartAsync()
        {
            var isSubmitted = await repository.SubmitCartAsync();

            if (isSubmitted == false)

                logger.LogError("error from AddCartItem, {0}", LogMessage.cartNotInDatabase);

            else
                SubmitToOtherSystem();

            return isSubmitted;
        }
        public void SubmitToOtherSystem()
        {
            // TODO: Data preparation and sending goes here
        }
    }
}
