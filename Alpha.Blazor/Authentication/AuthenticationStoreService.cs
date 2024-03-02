using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Alpha.Blazor.Authentication;

public interface IAuthenticationStoreService
{
    Task<AuthenticationModel?> GetAuthenticationModel();
    Task UpdateAuthenticationState(AuthenticationModel? authenticationModel);
    Task RemoveAuthenticationState();
}

public class AuthenticationStoreService(ILocalStorageService localStorageService) : IAuthenticationStoreService
{
    private readonly string storageItem = "Authentication";

    public async Task<AuthenticationModel?> GetAuthenticationModel()
    {
        return await localStorageService.GetItemAsync<AuthenticationModel>(storageItem);
    }

    public async Task UpdateAuthenticationState(AuthenticationModel? authenticationModel)
    {
        await localStorageService.SetItemAsync(storageItem, authenticationModel);
    }

    public async Task RemoveAuthenticationState()
    {
        await localStorageService.RemoveItemAsync(storageItem);
    }

}

