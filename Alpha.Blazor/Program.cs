using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Alpha.Blazor.Authentication;
namespace Alpha.Blazor;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddHttpClient<HttpClient>(HttpClientTypes.Gateway, client =>
        {
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        }).AddHttpMessageHandler<BearerTokenHandler>();

        builder.Services.AddScoped<BearerTokenHandler>();
        
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, AlphaAuthenticationStateProvider>();
        builder.Services.AddScoped<IAuthenticationStoreService, AuthenticationStoreService>();
        builder.Services.AddScoped<AlphaAuthenticationStateProvider>();
        builder.Services.AddBlazorBootstrap();
        
        await builder.Build().RunAsync();
    }
}