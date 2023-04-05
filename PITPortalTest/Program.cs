using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PITPortalTest;
using PITPortalTest.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMicrosoftGraphClient("https://graph.microsoft.com/User.Read");

builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.LoginMode = "Redirect";
    options.ProviderOptions.Cache.CacheLocation = "localStorage";
    options.ProviderOptions.Cache.StoreAuthStateInCookie = true;
    options.UserOptions.RoleClaim = "appRole";
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
})
.AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

builder.Services.AddScoped<CustomAccountFactory>();
builder.Services.AddSingleton<ICustomPages, CustomPages>();
builder.Services.AddBlazoredLocalStorageAsSingleton();


builder.Services.AddPolicies();


await builder.Build().RunAsync();