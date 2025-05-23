﻿namespace GraphWebApi.Service;

// https://dzone.com/articles/getting-access-token-for-microsoft-graph-using-oau

// https://developer.microsoft.com/en-us/graph/graph-explorer

internal class GraphService(Uri host, IAuthenticator? authenticator, string appName) 
    : JsonService(host, authenticator, appName, SourceGenerationContext.Default)
{
    protected override string? AuthenticationTestUrl => "/v1.0/me";

    #region error handling

    protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    {
        //var error = await ReadFromJsonAsync<ErrorRoot>(response, cancellationToken);

        JsonTypeInfo<ErrorRoot> jsonTypeInfoOut = (JsonTypeInfo<ErrorRoot>)context.GetTypeInfo(typeof(ErrorRoot))!;
        var error = await response.Content.ReadFromJsonAsync<ErrorRoot>(jsonTypeInfoOut, cancellationToken);

        WebServiceException.ThrowHttpError(error?.ToString(), response, memberName);
    }

    #endregion

    public async Task<UserModel?> CurrentUserAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<UserModel>("/v1.0/me", cancellationToken);
        return res;
    }

    // https://learn.microsoft.com/en-us/graph/api/user-sendmail?view=graph-rest-1.0&tabs=http
    public async Task SendMailAsync(MessageModel message, CancellationToken cancellationToken)
    {
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


    // https://learn.microsoft.com/en-us/graph/api/chat-post-messages?view=graph-rest-1.0&tabs=http
    public async Task SendChatMessage(string chatId, string message, CancellationToken cancellationToken)
    {
        var req = new MessageModel { Body = new BodyModel { Content = message } };
        await PostAsJsonAsync<MessageModel>($"/v1.0/chats/{chatId}/messages", req, cancellationToken);
    }

}
