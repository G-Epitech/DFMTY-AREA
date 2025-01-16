import 'dart:async';

import 'package:flutter/material.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/authentication/authentication.repository.dart';
import 'package:triggo/repositories/authentication/google.repository.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';

enum AuthenticationStatus {
  unknown,
  authenticated,
  authenticating,
  unauthenticated
}

class AuthenticationMediator with ChangeNotifier {
  final AuthenticationRepository _authenticationRepository;
  final CredentialsRepository _credentialsRepository;
  final GoogleRepository _googleRepository;

  AuthenticationMediator(this._authenticationRepository,
      this._credentialsRepository, this._googleRepository);

  final _controller = StreamController<AuthenticationStatus>();

  Stream<AuthenticationStatus> get status async* {
    await Future<void>.delayed(const Duration(seconds: 1));
    await refreshToken();
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
      // Display error message with a snackbar or dialog (something like that)
      _controller.add(AuthenticationStatus.unauthenticated);
      rethrow;
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
      // Display error message with a snackbar or dialog (something like that)
      _controller.add(AuthenticationStatus.unauthenticated);
      rethrow;
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
      // Display error message with a snackbar or dialog (something like that)
      _controller.add(AuthenticationStatus.unauthenticated);
    }
  }

  Future<void> logout() async {
    try {
      await _credentialsRepository.deleteTokens();
      _controller.add(AuthenticationStatus.unauthenticated);
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
    }
  }

  Future<bool?> authenticateWithGoogle() async {
    try {
      final res = await _googleRepository.getGoogleOAuth2Configuration();
      if (res.statusCode == Codes.ok) {
        _controller.add(AuthenticationStatus.authenticating);
        final GoogleSignIn googleSignIn =
            GoogleSignIn(scopes: res.data!.scopes);
        final googleAccount = await googleSignIn.signIn();
        final googleAuth = await googleAccount?.authentication;

        final credentials = await _googleRepository.getGoogleOAuth2Credentials(
            googleAuth?.accessToken ?? '', '.', 'Bearer');
        if (credentials.statusCode == Codes.ok) {
          await _credentialsRepository
              .saveAccessToken(credentials.data!.accessToken);
          await _credentialsRepository
              .saveRefreshToken(credentials.data!.refreshToken);
          _controller.add(AuthenticationStatus.authenticated);
          return true;
        } else {
          _controller.add(AuthenticationStatus.unauthenticated);
          throw Exception(credentials.message);
        }
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      _controller.add(AuthenticationStatus.unauthenticated);
      return false;
    }
  }
}
