using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Alpha.Blazor.Authentication;
using Refit;
using System.Security.Principal;
using Alpha.Common.Identity;
using Alpha.Common.WeatherService;
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

        builder.Services.AddRefitClient<IIdentityService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/identity"))
            .AddHttpMessageHandler<BearerTokenHandler>();

        builder.Services.AddRefitClient<IWeatherService>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/weather"))
            .AddHttpMessageHandler<BearerTokenHandler>();

        builder.Services.AddTransient<BearerTokenHandler>();
        
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, AlphaAuthenticationStateProvider>();
        builder.Services.AddScoped<IAuthenticationStoreService, AuthenticationStoreService>();
        builder.Services.AddScoped<AlphaAuthenticationStateProvider>();
        builder.Services.AddBlazorBootstrap();
        
        await builder.Build().RunAsync();
    }
}
