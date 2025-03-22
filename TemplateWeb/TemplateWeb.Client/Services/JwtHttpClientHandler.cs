using System.Net;
using System.Net.Http.Headers;

namespace TemplateWeb.Client.Services;

public class JwtHttpClientHandler : HttpClientHandler
{
    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = JwtAuthService.Token;
        var authHeader = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Authorization = authHeader;
        var response = await base.SendAsync(request, cancellationToken);
        
        return response;
    }
}