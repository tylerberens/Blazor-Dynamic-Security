using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace PITPortalTest.Services
{
    public class HasRolesFromConfigRequirement : IAuthorizationRequirement
    {
        public class HasRolesFromConfigHandler : AuthorizationHandler<HasRolesFromConfigRequirement>
        {
            private readonly ILocalStorageService _service;
            public HasRolesFromConfigHandler(ILocalStorageService service)
            {
                _service = service;
            }
            protected override async Task<Task> HandleRequirementAsync(
                AuthorizationHandlerContext context,
                HasRolesFromConfigRequirement requirement)
            {

                if (context.Resource is RouteData rd)
                {
                    var routeValue = rd.PageType.Name;
                    var pages = await _service.GetItemAsync<List<Page>>("Pages");

                    var selectedPage = pages.Where(x => x.RouteValue == routeValue).FirstOrDefault();
                    if (selectedPage != null)
                    {
                        var hasRequiredRole = selectedPage.AuthorizedRoles.Any(x => context.User.IsInRole(x));
                        if (hasRequiredRole)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                        }
                    }
                    else
                    {
                        context.Fail();
                    }
                }
                return Task.CompletedTask;
            }
        }
    }
}
