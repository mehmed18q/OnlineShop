using System.Security.Claims;

namespace Infrastructure.Utilities
{
    public static class IdentityUtilities
    {
        public const string UserIdClaimIdentity = "UserId";
        public const string TimeoutClaimIdentity = "TimeOut-Minute";

        public static TResult? GetClaim<TResult>(this ClaimsPrincipal user, string type)
        {
            string? result = user.Claims.FirstOrDefault(c => c.Type == type)?.Value;
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    if (typeof(TResult) == typeof(Guid) || typeof(TResult) == typeof(Guid?))
                    {
                        _ = Guid.TryParse(result, out Guid castedResult);
                        return (TResult?)(object)castedResult;
                    }
                    else
                    {
                        return (TResult?)Convert.ChangeType(result, typeof(TResult));
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"GetClaim: {result}");
                }
            }

            return default;
        }


        public static string GetPermissionCacheKey(this Guid userId)
        {
            return $"permissions-{userId}";
        }
    }
}
