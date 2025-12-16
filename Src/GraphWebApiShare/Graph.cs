namespace GraphWebApi;

public sealed class Graph : JsonService
{
    public Graph(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    public Graph(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    protected override string? AuthenticationTestUrl => "/v1.0/me";

    /// <summary>
    /// Configures the provided <see cref="HttpClient"/> instance with specific default headers required for API requests.
    /// This includes setting the User-Agent, Accept, and API version headers.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> to configure for GitHub API usage.</param>
    /// <param name="appName">The name of the application, used as the User-Agent header value.</param>
    protected override void InitializeClient(HttpClient client, string appName)
    {
        client.DefaultRequestHeaders.Add("User-Agent", appName);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    #region error handling

    protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    {
        //var error = await ReadFromJsonAsync<ErrorRoot>(response, cancellationToken);

        JsonTypeInfo<ErrorRoot> jsonTypeInfoOut = (JsonTypeInfo<ErrorRoot>)context.GetTypeInfo(typeof(ErrorRoot))!;
        var error = await response.Content.ReadFromJsonAsync<ErrorRoot>(jsonTypeInfoOut, cancellationToken);

        WebServiceException.ThrowHttpError(error?.ToString(), response, memberName);
    }

    #endregion

    //public Graph(Uri host, string token, string appName)
    //{
    //    service = new GraphService(host, new BearerAuthenticator(token), appName);
    //}

    public async Task<UserModel?> CurrentUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<UserModel>("/v1.0/me", cancellationToken);
        return res;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <permission>Mail.Send/*</permission>
    public async Task SendTextMail(string address, string subject, string body, CancellationToken cancellationToken = default)
    {
        // https://learn.microsoft.com/en-us/graph/api/user-sendmail?view=graph-rest-1.0&tabs=http
        WebServiceException.ThrowIfNotConnected(client);

        var message = new MessageModel()
        {
            Subject = subject,
            Body = new BodyModel { ContentType = "Text", Content = body },
            ToRecipients = [ new ToRecipientsModel { EmailAddress = new EmailAddressModel { Address = address } } ]
        };
        var req = new MessageRoot() { Message = message };
        await PostAsJsonAsync<MessageRoot>("/v1.0/me/sendMail", req, cancellationToken);
    }

   

    public async Task SendTextMail(IEnumerable<string> addresses, string subject, string body, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var message = new MessageModel()
        {
            Subject = subject,
            Body = new BodyModel { ContentType = "Text", Content = body },
            ToRecipients = addresses.Select(a => new ToRecipientsModel { EmailAddress = new EmailAddressModel { Address = a } }).ToArray()
        };
        var req = new MessageRoot() { Message = message };
        await PostAsJsonAsync<MessageRoot>("/v1.0/me/sendMail", req, cancellationToken);
    }

    // https://learn.microsoft.com/en-us/graph/api/chat-list?view=graph-rest-1.0&tabs=http#Code-try-3
    //public IAsyncEnumerable<> GetChatsAsync(CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<>("/v1.0/chats", cancellationToken);
    //}

    // https://learn.microsoft.com/en-us/graph/api/chat-list-messages?view=graph-rest-1.0&tabs=http
    //public IAsyncEnumerable<MessageModel> GetChatMessagesAsync(string chatId, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<>("/v1.0/chats/{chat-id}/messages", cancellationToken);
    //}

    public async Task SendChatMessage(string chatId, string message, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = new MessageModel { Body = new BodyModel { Content = message } };
        await PostAsJsonAsync<MessageModel>($"/v1.0/chats/{chatId}/messages", req, cancellationToken);
    }

    //Chat.ReadBasic, Chat.Read, Chat.ReadWrite


    // ChatMessage.Send
}