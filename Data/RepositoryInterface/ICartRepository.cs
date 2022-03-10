using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.RepositoryInterface
{
    public interface ICartRepository
    {
        public Task<Cart> GetCartAsync();
        public Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        public Task<List<CartItem>> GetCartItemsAsync();
        public Task<CartItem> AddCartItemAsync(CartItem cartItem);
        public Task<bool> DeleteCartItemAsync(int cartItemId);
        public Task<bool> CancelCartAsync();
        public Task<bool> SubmitCartAsync();
    }
}
