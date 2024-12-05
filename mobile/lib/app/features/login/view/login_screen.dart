import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/login/bloc/login_bloc.dart';
import 'package:triggo/app/features/login/view/email.screen.dart';
import 'package:triggo/app/features/login/view/password.screen.dart';
import 'package:triggo/mediator/authentication.mediator.dart';

class LoginScreen extends StatelessWidget {
  const LoginScreen({super.key});

  static Route<void> route() {
    return MaterialPageRoute<void>(builder: (_) => const LoginScreen());
  }

  @override
  Widget build(BuildContext context) {
    final authenticationMediator =
        RepositoryProvider.of<AuthenticationMediator>(context);

    return BlocProvider(
      create: (context) =>
          LoginBloc(authenticationMediator: authenticationMediator),
      child: const _LoginNavigator(),
    );
  }
}

class _LoginNavigator extends StatelessWidget {
  const _LoginNavigator();

  @override
  Widget build(BuildContext context) {
    return Navigator(
      onGenerateRoute: (settings) {
        switch (settings.name) {
          case '/email':
            return MaterialPageRoute(builder: (_) => const EmailInputScreen());
          case '/password':
            return MaterialPageRoute(
                builder: (_) => const PasswordInputScreen());
          default:
            return MaterialPageRoute(builder: (_) => const EmailInputScreen());
        }
      },
    );
  }
}
