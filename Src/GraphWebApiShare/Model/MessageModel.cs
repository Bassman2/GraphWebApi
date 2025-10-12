namespace GraphWebApi.Model;


internal class MessageRoot
{
    [JsonPropertyName("message")]
    public MessageModel? Message { get; set; }
}

internal class MessageModel
{
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("body")]
    public BodyModel? Body { get; set; }

    [JsonPropertyName("toRecipients")]
    public ToRecipientsModel[]? ToRecipients { get; set; }
}

internal class BodyModel
{
    [JsonPropertyName("contentType")]
    public string? ContentType { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

}

internal class ToRecipientsModel
{
    [JsonPropertyName("emailAddress")]
    public EmailAddressModel? EmailAddress { get; set; }
}

internal class EmailAddressModel
{
    [JsonPropertyName("address")]
    public string? Address { get; set; }
}


/*
{
    "message": {
        "subject": "Meet for lunch?",
        "body": {
            "contentType": "Text",
            "content": "The new cafeteria is open."
        },
        "toRecipients": [
            {
                "emailAddress": {
                    "address": "ralf.beckers@hotmail.com"
                }
            }
        ]
    }
}
*/