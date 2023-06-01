using Microsoft.AspNetCore.Components;
using IdentityModel.Client;

namespace Client.Pages;

public partial class Index
{
    [Inject]
    public IHttpClientFactory ClientFactory {get; set;}
    
    public string Content { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var identityClient = ClientFactory.CreateClient();
        var discDoc = await identityClient.GetDiscoveryDocumentAsync("http://localhost:5260");

        var tokenResult = await identityClient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest()
            {
                Address = discDoc.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "secret",
                Scope = "read"
            });

        var apiClient = ClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResult.AccessToken);
        var content = await apiClient.GetStringAsync("http://localhost:5237/Secret");
        Content = content;

        StateHasChanged();
    }
}