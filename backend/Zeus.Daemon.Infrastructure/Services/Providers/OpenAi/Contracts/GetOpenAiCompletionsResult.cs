namespace Zeus.Daemon.Infrastructure.Services.Providers.OpenAi.Contracts;

public record GetOpenAiCompletionsResult(
    string Id,
    string Object,
    string Model,
    List<GetOpenAiCompletionsChoice> Choices);

public record GetOpenAiCompletionsChoice(
    int Index,
    GetOpenAiCompletionsChoiceMessage Message);

public record GetOpenAiCompletionsChoiceMessage(
    string Role,
    string Content);
