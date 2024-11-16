namespace GraphWebApi.Service.Model;

internal class ErrorModel
{
    [JsonPropertyName("error")]
    public ErrorItem? Error { get; set; }
}

internal class ErrorItem
{
    [JsonPropertyName("code")]
    public string? code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

/*

{
"error": {
"code": "InvalidAuthenticationToken",
"message": "IDX14100: JWT is not well formed, there are no dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EncodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.",
"innerError": {
  "date": "2024-11-16T11:19:42",
  "request-id": "431f5273-714f-4132-9dff-25e9509bc705",
  "client-request-id": "431f5273-714f-4132-9dff-25e9509bc705"
}
}
}

*/