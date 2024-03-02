using System.Data;
using System.Net.Http.Headers;

namespace Alpha.Blazor.Authentication;

public class BearerTokenHandler(IAuthenticationStoreService storeService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var model = await storeService.GetAuthenticationModel();
        
        if( model != null )
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);

        return await base.SendAsync(request, cancellationToken);
    }
}