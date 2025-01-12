import 'dart:async';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_appauth/flutter_appauth.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/google.repository.dart';

class GoogleMediator with ChangeNotifier {
  final GoogleRepository _googleRepository;
  final CredentialsRepository _credentialsRepository;
  GoogleMediator(this._googleRepository, this._credentialsRepository);

  //StreamSubscription? _linkSubscription;
  final FlutterAppAuth appAuth = FlutterAppAuth();

  Future<void> refreshAccessToken() async {
    try {
      final res = await _googleRepository.getGoogleOAuth2Configuration();
      if (res.statusCode == Codes.ok) {
        final clientId = res.data!.clientIds.first['clientId'];
        final redirectUri = 'com.triggo.oauth2://oauth2callback';
        final String? refreshToken =
            await _credentialsRepository.getRefreshToken();

        if (refreshToken != null) {
          final TokenResponse result = await appAuth.token(TokenRequest(
            clientId,
            redirectUri,
            refreshToken: refreshToken,
          ));

          _credentialsRepository.saveAccessToken(result.accessToken!);
          _credentialsRepository.saveRefreshToken(result.refreshToken!);

          print('New Access token: ${result.accessToken}');
          print('New Refresh token: ${result.refreshToken}');
        }
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      print('Error refreshing access token: $e');
    }
  }

  Future<void> authenticateWithGoogle() async {
    try {
      final res = await _googleRepository.getGoogleOAuth2Configuration();
      if (res.statusCode == Codes.ok) {
        final scopes = res.data!.scopes;
        final authorizationEndpoint = res.data!.endpoint;
        final redirectUri = 'com.triggo.oauth2://oauth2callback';
        String clientId = '';
        print('Scopes: $scopes');
        print('Authorization endpoint: $authorizationEndpoint');
        print('Client IDs: ${res.data!.clientIds}');
        for (final client in res.data!.clientIds) {
          if ((Platform.isAndroid && client['provider'] == 'Android') ||
              (Platform.isIOS && client['provider'] == 'Ios')) {
            clientId = client['clientId'];
            break;
          }
        }
        print('Client ID: $clientId');

        GoogleSignIn googleSignIn = GoogleSignIn(
          scopes: scopes,
        );
        final googleAccount = await googleSignIn.signIn();
        final googleAuth = await googleAccount?.authentication;

        print('Access token: ${googleAuth?.accessToken}');
        print('ID token: ${googleAuth?.idToken}');

        //----------------------------------------

        /*final AuthorizationTokenResponse result =
            await appAuth.authorizeAndExchangeCode(
          AuthorizationTokenRequest(
            clientId,
            redirectUri,
            scopes: scopes,
            serviceConfiguration: AuthorizationServiceConfiguration(
              authorizationEndpoint: authorizationEndpoint,
              tokenEndpoint: 'https://oauth2.googleapis.com/token',
            ),
          ),
        );

        print('Access token: ${result.accessToken}');
        print('Refresh token: ${result.refreshToken}');*/

        // Send access token to the backend

        //----------------------------------------

        /*String buildAuthorizationUrl({
          required String endpoint,
          required String clientId,
          required String redirectUri,
          required List<String> scopes,
        }) {
          final scopeString = Uri.encodeComponent(scopes.join(' '));

          return '$endpoint'
              '&response_type=code'
              '&client_id=$clientId'
              '&redirect_uri=$redirectUri'
              '&scope=$scopeString';
        }

        final authorizationUrl = buildAuthorizationUrl(
          endpoint: 'https://accounts.google.com/o/oauth2/auth',
          clientId: clientId,
          redirectUri: redirectUri,
          scopes: scopes,
        );
        print('Authorization URL: $authorizationUrl');
        launchURL(authorizationUrl);*/
        /*startListeningForDeepLinks((code) {
          print('Code received: $code');
        });*/
        // Send code to the backend
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      print('Error connecting to Google: $e');
      // Display error message with a snackbar or dialog (something like that)
    }
  }

  /*void startListeningForDeepLinks(Function(String) onCodeReceived) {
    _linkSubscription = uriLinkStream.listen((Uri? uri) {
      if (uri != null && uri.scheme == 'com.triggo.oauth2') {
        final code = uri.queryParameters['code'];
        if (code != null) {
          onCodeReceived(code);
          _linkSubscription
              ?.cancel(); // Stop listening once the code is received
        }
      }
    }, onError: (err) {
      print('Error listening for deep links: $err');
    });
  }*/

  /*void stopListeningForDeepLinks() {
    _linkSubscription?.cancel();
  }*/
}
