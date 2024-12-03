import 'dart:async';

import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/authentication.repository.dart';
import 'package:triggo/repositories/credentials.repository.dart';

enum AuthenticationStatus {
  unknown,
  authenticated,
  authenticating,
  unauthenticated
}

class AuthenticationMediator with ChangeNotifier {
  final AuthenticationRepository _authenticationRepository;
  final CredentialsRepository _credentialsRepository;

  AuthenticationMediator(
      this._authenticationRepository, this._credentialsRepository);

  final _controller = StreamController<AuthenticationStatus>();

  Stream<AuthenticationStatus> get status async* {
    await Future<void>.delayed(const Duration(seconds: 1));
    yield AuthenticationStatus.unknown;
    yield* _controller.stream;
  }

  Future<void> login(String email, String password) async {
    try {
      _controller.add(AuthenticationStatus.authenticating);
      final res = await _authenticationRepository.login(email, password);

      if (res.statusCode == Codes.ok) {
        await _credentialsRepository.saveAccessToken(res.data!.accessToken);
        await _credentialsRepository.saveRefreshToken(res.data!.refreshToken);
        _controller.add(AuthenticationStatus.authenticated);
      } else {
        _controller.add(AuthenticationStatus.unauthenticated);
        throw Exception(res.message);
      }
    } catch (e) {
      print("Error: $e");
      // Display error message with a snackbar or dialog (something like that)
    }
  }

  Future<void> register(
      String email, String password, String firstName, String lastName) async {
    try {
      _controller.add(AuthenticationStatus.authenticating);
      final res = await _authenticationRepository.register(
          email, password, firstName, lastName);

      if (res.statusCode == Codes.created) {
        await _credentialsRepository.saveAccessToken(res.data!.accessToken);
        await _credentialsRepository.saveRefreshToken(res.data!.refreshToken);
        _controller.add(AuthenticationStatus.authenticated);
      } else {
        _controller.add(AuthenticationStatus.unauthenticated);
        throw Exception(res.message);
      }
    } catch (e) {
      print("Error: $e");
      // Display error message with a snackbar or dialog (something like that)
    }
  }

  Future<void> refreshToken() async {
    try {
      _controller.add(AuthenticationStatus.authenticating);
      final refreshToken = await _credentialsRepository.getRefreshToken();
      if (refreshToken == null) {
        _controller.add(AuthenticationStatus.unauthenticated);
        throw Exception('No refresh token found');
      }
      final res = await _authenticationRepository.refreshToken(refreshToken);

      if (res.statusCode == Codes.ok) {
        await _credentialsRepository.saveAccessToken(res.data!.accessToken);
        await _credentialsRepository.saveRefreshToken(res.data!.refreshToken);
        _controller.add(AuthenticationStatus.authenticated);
      } else {
        _controller.add(AuthenticationStatus.unauthenticated);
        throw Exception(res.message);
      }
    } catch (e) {
      print("Error: $e");
      // Display error message with a snackbar or dialog (something like that)
    }
  }
}
