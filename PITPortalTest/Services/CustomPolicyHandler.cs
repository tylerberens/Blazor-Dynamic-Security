using Microsoft.AspNetCore.Authorization;

namespace PITPortalTest.Services
{
    public static class AuthorizationOptionsPolicyExtensions
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, HasRolesFromConfigRequirement.HasRolesFromConfigHandler>();
            services.AddAuthorizationCore(options => options.ConfigurePolicies());
        }

        public static AuthorizationOptions ConfigurePolicies(this AuthorizationOptions options)
        {
            options.AddPolicy("PagePolicy", policy => policy.Requirements.Add(new HasRolesFromConfigRequirement()));
            return options;
        }
    }
}
