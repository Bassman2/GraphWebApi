namespace GraphWebApi;


public sealed class Graph(Uri uri, string apiKey) : IDisposable
{
    private GraphService? service = new(uri, apiKey);

    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

    public async Task<UserModel?> CurrentUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        return await service!.CurrentUserAsync(cancellationToken);
    }
}