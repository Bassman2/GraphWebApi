
namespace GraphWebApi.Service;


// https://developer.microsoft.com/en-us/graph/graph-explorer

internal class GraphService(Uri host, string apiKey) : JsonService(host, SourceGenerationContext.Default, new BearerAuthenticator(apiKey))
{
    //public IEnumerable<ProjectModel>? GetProjects()
    //{
    //    return GetFromJson<List<ProjectModel>>("/access/api/v1/projects");
    //}

    public async Task<UserModel?> CurrentUserAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<UserModel>("/v1.0/me", cancellationToken);
        return res;
    }
}
