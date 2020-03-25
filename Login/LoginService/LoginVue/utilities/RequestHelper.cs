using Microsoft.AspNetCore.Http;
using System;

namespace LoginVue.utilities
{
    /// <summary>
    /// This code is a copy from 
    /// https://stackoverflow.com/questions/43138659/request-isajaxrequest-alternative-in-mvc6
    /// </summary>
    internal static class RequestHelpers
    {
        internal static bool IsAjaxRequest(this HttpRequest request)
        {
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                   string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                   string.Equals(request.Headers["X-Requested-With"], "Fetch", StringComparison.Ordinal);
        }
    }
}