using Data.RepositoryInterface;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.RepositoryImplementation
{
    public class AccountRepository: IAccountRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IList<string>> LoginAsync(string username, string password)
        {
            var identityUser = await userManager.FindByNameAsync(username);

            if(identityUser != null && await userManager.CheckPasswordAsync(identityUser, password))
            
                return await userManager.GetRolesAsync(identityUser);

            return null;
            
        }
    }
}
