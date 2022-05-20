using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationFilter()
        {
#if Dummy
            _authenticationService = new DummyServices.DummyAuthenticationService();
#else
            _authorizationService = null;
#endif
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            StringValues bearerTokenHeader = context.HttpContext.Request.Headers["InternalBearerToken"];
            StringValues internalIdHeader = context.HttpContext.Request.Headers["InternalUserId"];
            if (string.IsNullOrWhiteSpace(bearerTokenHeader) | string.IsNullOrWhiteSpace(internalIdHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            bool isUserAuthorized = await _authenticationService.IsAuthenticated(bearerTokenHeader, internalIdHeader);
            if (!isUserAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }

            return;
        }
    }
}
