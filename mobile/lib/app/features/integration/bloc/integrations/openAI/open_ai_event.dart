part of 'open_ai_bloc.dart';

sealed class OpenAIEvent extends Equatable {
  const OpenAIEvent();

  @override
  List<Object> get props => [];
}

final class OpenAIAPIKeyChanged extends OpenAIEvent {
  const OpenAIAPIKeyChanged(this.apiToken);

  final String apiToken;

  @override
  List<Object> get props => [apiToken];
}

final class OpenAIAdminAPIKeyChanged extends OpenAIEvent {
  const OpenAIAdminAPIKeyChanged(this.adminApiToken);

  final String adminApiToken;

  @override
  List<Object> get props => [adminApiToken];
}

final class OpenAISubmitted extends OpenAIEvent {
  const OpenAISubmitted();
}

final class OpenAIReset extends OpenAIEvent {
  const OpenAIReset();
}
