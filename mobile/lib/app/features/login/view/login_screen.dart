import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/login/bloc/login_bloc.dart';
import 'package:triggo/app/features/login/view/login_form.dart';
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

    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: BlocProvider(
          create: (context) => LoginBloc(
            authenticationMediator: authenticationMediator,
          ),
          child: const LoginForm(),
        ),
      ),
    );
  }
}
