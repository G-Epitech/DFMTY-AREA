import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/integration/models/models.dart';
import 'package:triggo/mediator/integrations/openAI.mediator.dart';

part 'open_ai_event.dart';
part 'open_ai_state.dart';

class OpenAIIntegrationBloc extends Bloc<OpenAIEvent, OpenAIIntegrationState> {
  OpenAIIntegrationBloc({
    required OpenAIMediator openAIMediator,
  })  : _openAIMediator = openAIMediator,
        super(const OpenAIIntegrationState()) {
    on<OpenAIAPIKeyChanged>(_onApiKeyChanged);
    on<OpenAIAdminAPIKeyChanged>(_onAdminApiKeyChanged);
    on<OpenAISubmitted>(_onSubmitted);
    on<OpenAIReset>(_onReset);
  }

  // Need to be removed

  final OpenAIMediator _openAIMediator;

  void _onApiKeyChanged(
    OpenAIAPIKeyChanged event,
    Emitter<OpenAIIntegrationState> emit,
  ) {
    final apiToken = ApiToken.dirty(event.apiToken);
    emit(
      state.copyWith(
        apiToken: apiToken,
        isValid: Formz.validate([apiToken, state.adminApiToken]),
      ),
    );
  }

  void _onAdminApiKeyChanged(
    OpenAIAdminAPIKeyChanged event,
    Emitter<OpenAIIntegrationState> emit,
  ) {
    final adminApiToken = AdminApiToken.dirty(event.adminApiToken);
    emit(
      state.copyWith(
        adminApiToken: adminApiToken,
        isValid: Formz.validate([adminApiToken, state.apiToken]),
      ),
    );
  }

  Future<void> _onSubmitted(
    OpenAISubmitted event,
    Emitter<OpenAIIntegrationState> emit,
  ) async {
    if (state.isValid) {
      emit(state.copyWith(status: FormzSubmissionStatus.inProgress));
      try {
        await _openAIMediator.linkAccount(
          state.apiToken.value,
          state.adminApiToken.value,
        );

        emit(state.copyWith(status: FormzSubmissionStatus.success));
      } catch (_) {
        emit(state.copyWith(status: FormzSubmissionStatus.failure));
      }
    }
  }

  Future<void> _onReset(
    OpenAIReset event,
    Emitter<OpenAIIntegrationState> emit,
  ) async {
    emit(
      state.copyWith(
        status: FormzSubmissionStatus.initial,
      ),
    );
  }
}
