using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Alpha.Blazor.Authentication;

public class AlphaAuthenticationStateProvider(IAuthenticationStoreService storeService) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var authenticationModel = await storeService.GetAuthenticationModel();

            if (authenticationModel == null) 
                return await Task.FromResult(new AuthenticationState(anonymous));

            return await Task.FromResult(new AuthenticationState(SetClaims(authenticationModel.Username!)));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(anonymous));
        }
    }

    public async Task UpdateAuthenticationState(AuthenticationModel? authenticationModel)
    {
        try
        {
            ClaimsPrincipal claimsPrincipal = new();

            if (authenticationModel is not null)
            {
                claimsPrincipal = SetClaims(authenticationModel.Username!);
                await storeService.UpdateAuthenticationState(authenticationModel);
            }
            else
            {
                await storeService.RemoveAuthenticationState();
                claimsPrincipal = anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        catch
        {
            await Task.FromResult(new AuthenticationState(anonymous));
        }
    }

    private static ClaimsPrincipal SetClaims(string email) => new(new ClaimsIdentity([new Claim(ClaimTypes.Name, email)], "AlphaAuth"));

}

