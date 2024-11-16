namespace GraphWebApi.Service.Model;

[JsonSourceGenerationOptions]

[JsonSerializable(typeof(ErrorRoot))]
[JsonSerializable(typeof(ErrorModel))]

[JsonSerializable(typeof(UserModel))]

[JsonSerializable(typeof(MessageRoot))]
[JsonSerializable(typeof(MessageModel))]
[JsonSerializable(typeof(BodyModel))]
[JsonSerializable(typeof(ToRecipientsModel))]
[JsonSerializable(typeof(EmailAddressModel))]

//[JsonSerializable(typeof(ProjectModel))]
//[JsonSerializable(typeof(RepositoryModel))]
//[JsonSerializable(typeof(List<RepositoryModel>))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}


