namespace GraphWebApi.Service.Model;

public class UserModel
{
    [JsonPropertyName("businessPhones")]
    public string[]? BusinessPhones { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("givenName")]
    public string? GivenName { get; set; }

    [JsonPropertyName("jobTitle")]
    public string? JobTitle { get; set; }

    [JsonPropertyName("mail")]
    public string? Mail { get; set; }

    [JsonPropertyName("mobilePhone")]
    public string? MobilePhone { get; set; }

    [JsonPropertyName("officeLocation")]
    public string? OfficeLocation { get; set; }

    [JsonPropertyName("preferredLanguage")]
    public string? PreferredLanguage { get; set; }

    [JsonPropertyName("surname")]
    public string? Surname { get; set; }

    [JsonPropertyName("userPrincipalName")]
    public string? UserPrincipalName { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

}


/*
{
"@odata.context": "https://graph.microsoft.com/v1.0/$metadata#users/$entity",
"@microsoft.graph.tips": "This request only returns a subset of the resource's properties. Your app will need to use $select to return non-default properties. To find out what other properties are available for this resource see https://learn.microsoft.com/graph/api/resources/user",
"businessPhones": [
    "+49 9131 7701 6156"
],
"displayName": "Beckers, Ralf",
"givenName": "Ralf",
"jobTitle": "Expert",
"mail": "Ralf.Beckers@elektrobit.com",
"mobilePhone": "+491735724844",
"officeLocation": "DEERL1-4BM09",
"preferredLanguage": null,
"surname": "Beckers",
"userPrincipalName": "Ralf.Beckers@elektrobit.com",
"id": "2f1da5eb-228b-464f-8756-9496461d9a05"
}
*/