using Data.Constants;
using Data.Models;
using Data.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.RepositoryImplementation
{
    public class CartRepository: ICartRepository
    {
        private readonly CartDBContext context;

        public CartRepository(CartDBContext context)
        {
            this.context = context;
        }
        public async Task<Cart> GetCartAsync()
        {
            return await context.Carts.FirstOrDefaultAsync();
        }
        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await context.CartItems.Where(item => item.Id == cartItemId).FirstOrDefaultAsync();   
        }
        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var carItems = context.CartItems;

            if (carItems == null)

                return null;

            return await context.CartItems.ToListAsync();
        }
        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            var cart = await context.Carts.FirstOrDefaultAsync();

            if (cart == null)

                return null;

            cart.Status = Status.Draft;

            await context.SaveChangesAsync();         

            cartItem.CartId = 1;

            context.CartItems.Add(cartItem);

            await context.SaveChangesAsync();
            
            return cartItem;
        }
        public async Task<bool> DeleteCartItemAsync(int cartItemId)
        {

            var cartItem = await context.CartItems.Where(item => item.Id == cartItemId).FirstOrDefaultAsync();

            if (cartItem == null)

                return false;

            context.Remove(cartItem);

            await context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> CancelCartAsync()
        {
            var cart = await context.Carts.FirstOrDefaultAsync();

            if (cart == null)

                return false;

            cart.Status = Status.Cancelled;

            await context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> SubmitCartAsync()
        {
            var cart = await context.Carts.FirstOrDefaultAsync();

            if (cart == null)

                return false;

            cart.Status = Status.Submitted;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
