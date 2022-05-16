using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationFilter()
        {
#if Dummy
            _authorizationService = new DummyServices.DummyAuthorizationService();
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
                context.Result = new BadRequestResult();
                return;
            }

            bool isUserAuthorized = await _authorizationService.Authorize(bearerTokenHeader, internalIdHeader);
            if (!isUserAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }

            return;
        }
    }
}
