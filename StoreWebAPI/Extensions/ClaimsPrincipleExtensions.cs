using System.Security.Claims;

namespace StoreWebAPI.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetEmailPrincipal(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.Email);
    }
}
