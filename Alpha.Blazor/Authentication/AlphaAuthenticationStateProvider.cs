using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Alpha.Blazor.Authentication;

public class AlphaAuthenticationStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
    private readonly ILocalStorageService localStorageService = localStorageService;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var authenticationModel = await localStorageService.GetItemAsStringAsync("Authentication");
            if (authenticationModel == null) { return await Task.FromResult(new AuthenticationState(anonymous)); }
            return await Task.FromResult(new AuthenticationState(SetClaims(Deserialize(authenticationModel).Username!)));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(anonymous));
        }
    }

    public async Task UpdateAuthenticationState(AuthenticationModel authenticationModel)
    {
        try
        {
            ClaimsPrincipal claimsPrincipal = new();
            if (authenticationModel is not null)
            {
                claimsPrincipal = SetClaims(authenticationModel.Username!);
                await localStorageService.SetItemAsStringAsync("Authentication", Serialize(authenticationModel));
            }
            else
            {
                await localStorageService.RemoveItemAsync("Authentication");
                claimsPrincipal = anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        catch
        {
            await Task.FromResult(new AuthenticationState(anonymous));
        }


    }

    private static ClaimsPrincipal SetClaims(string email) => new(new ClaimsIdentity([new Claim(ClaimTypes.Name, email)], "CustomAuth"));
    private static AuthenticationModel Deserialize(string serializeString) => JsonSerializer.Deserialize<AuthenticationModel>(serializeString)!;
    private static string Serialize(AuthenticationModel model) => JsonSerializer.Serialize(model);
}

