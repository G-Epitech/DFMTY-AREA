part of 'open_ai_bloc.dart';

final class OpenAIIntegrationState extends Equatable {
  const OpenAIIntegrationState({
    this.status = FormzSubmissionStatus.initial,
    this.apiToken = const ApiToken.pure(),
    this.adminApiToken = const AdminApiToken.pure(),
    this.isValid = false,
  });

  final FormzSubmissionStatus status;
  final ApiToken apiToken;
  final AdminApiToken adminApiToken;
  final bool isValid;

  OpenAIIntegrationState copyWith({
    FormzSubmissionStatus? status,
    ApiToken? apiToken,
    AdminApiToken? adminApiToken,
    bool? isValid,
  }) {
    return OpenAIIntegrationState(
      status: status ?? this.status,
      apiToken: apiToken ?? this.apiToken,
      adminApiToken: adminApiToken ?? this.adminApiToken,
      isValid: isValid ?? this.isValid,
    );
  }

  @override
  List<Object> get props => [status, apiToken, adminApiToken];
}
