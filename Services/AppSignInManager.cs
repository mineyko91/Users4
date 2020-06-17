using _4zadanie.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace _4zadanie.Services
{
    public class AppSignInManager : SignInManager<User>
    {
        public AppSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {

        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            var user = UserManager.FindByNameAsync(userName).Result;


            if (user == null ||
                string.IsNullOrEmpty(user.UserName) ||
                !user.IsEnabled)
            {
                return Task.FromResult(SignInResult.Failed);
            }
            return base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }
}
