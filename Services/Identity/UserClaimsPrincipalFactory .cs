using MCS.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace MCS.Models
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<DeptStaff, ApplicationRole>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<DeptStaff> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(DeptStaff user)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) }, CookieAuthenticationDefaults.AuthenticationScheme);

            // Add base claims using the UserManager
            identity.AddClaims(await UserManager.GetClaimsAsync(user));

            // Add custom claims
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role ?? ""));

            return identity;
        }
    }
}
