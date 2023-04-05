using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

public class CustomAccountFactory
    : AccountClaimsPrincipalFactory<CustomUserAccount>
{
	private readonly ILogger<CustomAccountFactory> _logger;
	private readonly IServiceProvider _serviceProvider;
	public static string? Token { get; private set; } = null;
	public CustomAccountFactory(IAccessTokenProviderAccessor accessor,
	    IServiceProvider serviceProvider,
	    ILogger<CustomAccountFactory> logger)
	    : base(accessor)
	{
		_serviceProvider = serviceProvider;
		_logger = logger;
    }
    public string? GetAudToken()
    {
        return Token;
    }
	
	public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
	    CustomUserAccount account,
	    RemoteAuthenticationUserOptions options)
	{
		var initialUser = await base.CreateUserAsync(account, options);

		if (initialUser.Identity is null)
		{
			return initialUser;
		}

		if (initialUser.Identity.IsAuthenticated)
		{
			var userIdentity = (ClaimsIdentity)initialUser.Identity;

			userIdentity.AddClaim(new Claim("aud", account.Aud));

			foreach (var role in account.Groups)
			{
				userIdentity.AddClaim(new Claim("groups", role));
			}
			foreach (var role in account.Roles)
			{
				userIdentity.AddClaim(new Claim("appRole", role));
			}

			foreach (var wid in account.Wids)
			{
				userIdentity.AddClaim(new Claim("directoryRole", wid));
			}
			Token = account.Aud;
		}

		return initialUser;
	}
}