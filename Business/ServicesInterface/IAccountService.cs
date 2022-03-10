using Business.HelperModels;
using System.Threading.Tasks;

namespace Business.ServicesInterface
{
    public interface IAccountService
    {
        public Task<string> LoginAsync(User user);
    }
}
