import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/login/models/models.dart';
import 'package:triggo/mediator/authentication.mediator.dart';

part 'register_event.dart';
part 'register_state.dart';

class LoginBloc extends Bloc<RegisterEvent, RegisterState> {
  LoginBloc({
    required AuthenticationMediator authenticationMediator,
  })  : _authenticationMediator = authenticationMediator,
        super(const RegisterState()) {
    on<LoginEmailChanged>(_onEmailChanged);
    on<LoginPasswordChanged>(_onPasswordChanged);
    on<LoginSubmitted>(_onSubmitted);
    on<LoginReset>(_onReset);
  }

  final AuthenticationMediator _authenticationMediator;

  void _onEmailChanged(
    LoginEmailChanged event,
    Emitter<RegisterState> emit,
  ) {
    final email = Email.dirty(event.email);
    emit(
      state.copyWith(
        email: email,
        isValid: Formz.validate([state.password, email]),
      ),
    );
  }

  void _onPasswordChanged(
    LoginPasswordChanged event,
    Emitter<RegisterState> emit,
  ) {
    final password = Password.dirty(event.password);
    emit(
      state.copyWith(
        password: password,
        isValid: Formz.validate([password, state.email]),
      ),
    );
  }

  Future<void> _onSubmitted(
    LoginSubmitted event,
    Emitter<RegisterState> emit,
  ) async {
    if (state.isValid) {
      emit(state.copyWith(status: FormzSubmissionStatus.inProgress));
      try {
        await _authenticationMediator.login(
          state.email.value,
          state.password.value,
        );

        emit(state.copyWith(status: FormzSubmissionStatus.success));
      } catch (_) {
        emit(state.copyWith(status: FormzSubmissionStatus.failure));
      }
    }
  }

  Future<void> _onReset(
    LoginReset event,
    Emitter<RegisterState> emit,
  ) async {
    emit(
      state.copyWith(
        status: FormzSubmissionStatus.initial,
      ),
    );
  }
}
