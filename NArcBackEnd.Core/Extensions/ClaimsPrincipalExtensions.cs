using System.Security.Claims;

namespace NArcBackEnd.Core.Extensions
{
    // Secured Aspect - 2
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            //name kısmını getirmek için 
            var result = claimsPrincipal?.FindAll(claimType)?.Select(p => p.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            //rolleri getirmek için!
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
