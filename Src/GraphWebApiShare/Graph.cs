namespace GraphWebApi;

public sealed class Graph(string apiKey) : IDisposable
{
    private GraphService? service = new(apiKey);

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <permission>Mail.Send/*</permission>
    public async Task SendTextMail(string address, string subject, string body, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var message = new MessageModel()
        {
            Subject = subject,
            Body = new BodyModel { ContentType = "Text", Content = body },
            ToRecipients = [ new ToRecipientsModel { EmailAddress = new EmailAddressModel { Address = address } } ]
        };
        await service!.SendMailAsync(message, cancellationToken);
    }

    public async Task SendTextMail(IEnumerable<string> addresses, string subject, string body, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var message = new MessageModel()
        {
            Subject = subject,
            Body = new BodyModel { ContentType = "Text", Content = body },
            ToRecipients = addresses.Select(a => new ToRecipientsModel { EmailAddress = new EmailAddressModel { Address = a } }).ToArray()
        };
        await service!.SendMailAsync(message, cancellationToken);
    }


    //Chat.ReadBasic, Chat.Read, Chat.ReadWrite


    // ChatMessage.Send
}