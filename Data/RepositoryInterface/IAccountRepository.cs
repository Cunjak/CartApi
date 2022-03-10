using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.RepositoryInterface
{
    public interface IAccountRepository
    {
        public Task<IList<string>> LoginAsync(string username, string password);
    }
}
