using Business.HelperModels;
using Data.Models;
using System.Threading.Tasks;

namespace Business.ServicesInterface
{
    public interface ICartService
    {
        public Task<CartOverview> GetCartOverviewAsync();
        public Task<string> GetCartItemByIdAsync(int cartItemId);
        public Task<CartItem> AddCartItemAsync(CartItemFromBody cartItem);
        public Task<bool> DeleteCartItemAsync(int cartItemId);
        public Task<bool> CancelCartAsync();
        public Task<bool> SubmitCartAsync();
    }
}
