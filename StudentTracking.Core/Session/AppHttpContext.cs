using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace StudentTracking.Core.Session
{
    public static class AppHttpContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;

        public static string GetUserAgent(HttpRequest? request = null)
        {
            request ??= Current.Request;
            return request.Headers[HeaderNames.UserAgent];
        }

        public static string GetToken(HttpRequest? request = null)
        {
            request ??= Current.Request;
            var values = request.Headers[HeaderNames.Authorization];

            var value = values.FirstOrDefault();

            if (value == null) return null;
            value = value.Replace("Bearer", "").Replace("JWT", "").Replace("null", "").Replace("undefined", "").Trim();

            return value;
        }
    }
}