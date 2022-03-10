using Business.HelperModels;
using Business.ServicesInterface;
using Data.RepositoryInterface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServiceImplementation
{
    public class AccountService: IAccountService
    {
        private readonly IConfiguration configuration;
        private readonly IAccountRepository repository;

        public AccountService(IConfiguration configuration, IAccountRepository repository)
        {
            this.configuration = configuration;
            this.repository = repository;
        }
        public async Task<string> LoginAsync(User user)
        {
            var userRoles = await repository.LoginAsync(user.Username, user.Password);

            if (userRoles == null)

                return null;

                var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
         
        }
    }
}
