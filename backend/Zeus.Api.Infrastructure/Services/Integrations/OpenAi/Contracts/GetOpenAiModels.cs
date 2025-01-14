namespace Zeus.Api.Infrastructure.Services.Integrations.OpenAi.Contracts;

public record GetOpenAiModels(
    string Object,
    List<GetOpenAiModel> Data);

public record GetOpenAiModel(
    string Id,
    string Object,
    long Created,
    string OwnedBy);
