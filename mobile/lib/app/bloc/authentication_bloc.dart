import 'dart:async';

import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/mediator/authentication.mediator.dart';
import 'package:triggo/models/user.model.dart';
import 'package:triggo/repositories/user/user.repository.dart';

part 'authentication_event.dart';
part 'authentication_state.dart';

class AuthenticationBloc
    extends Bloc<AuthenticationEvent, AuthenticationState> {
  AuthenticationBloc({
    required AuthenticationMediator authenticationMediator,
    required UserRepository userRepository,
  })  : _authenticationMediator = authenticationMediator,
        _userRepository = userRepository,
        super(const AuthenticationState.unknown()) {
    on<AuthenticationSubscriptionRequested>(_onSubscriptionRequested);
    on<AuthenticationLogoutPressed>(_onLogoutPressed);
  }

  final AuthenticationMediator _authenticationMediator;
  final UserRepository _userRepository;

  Future<void> _onSubscriptionRequested(
    AuthenticationSubscriptionRequested event,
    Emitter<AuthenticationState> emit,
  ) {
    return emit.onEach(
      _authenticationMediator.status,
      onData: (status) async {
        switch (status) {
          case AuthenticationStatus.unauthenticated:
            return emit(const AuthenticationState.unauthenticated());
          case AuthenticationStatus.authenticated:
            final user = await _tryGetUser();
            return emit(
              user != null
                  ? AuthenticationState.authenticated(user)
                  : const AuthenticationState.unauthenticated(),
            );
          case AuthenticationStatus.unknown:
            return emit(const AuthenticationState.unknown());
          case AuthenticationStatus.authenticating:
            return emit(const AuthenticationState.authenticating());
        }
      },
      onError: addError,
    );
  }

  void _onLogoutPressed(
    AuthenticationLogoutPressed event,
    Emitter<AuthenticationState> emit,
  ) {
    _authenticationMediator.logout();
  }

  Future<User?> _tryGetUser() async {
    try {
      final res = await _userRepository.getUser();

      if (res.statusCode == Codes.ok && res.data != null) {
        return User(
          firstName: res.data!.firstName,
          lastName: res.data!.lastName,
          email: res.data!.email,
          picture: res.data!.picture,
        );
      }
      return null;
    } catch (_) {
      return null;
    }
  }
}
