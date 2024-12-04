import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/bloc/authentication_bloc.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  static Route<void> route() {
    return MaterialPageRoute<void>(builder: (_) => const HomePage());
  }

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [_UserId(), _LogoutButton()],
        ),
      ),
    );
  }
}

class _LogoutButton extends StatelessWidget {
  const _LogoutButton();

  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      child: const Text('Logout'),
      onPressed: () {
        context.read<AuthenticationBloc>().add(AuthenticationLogoutPressed());
      },
    );
  }
}

class _UserId extends StatelessWidget {
  const _UserId();

  @override
  Widget build(BuildContext context) {
    final userFirstName = context.select(
      (AuthenticationBloc bloc) => bloc.state.user.firstName,
    );
    final userLastName = context.select(
      (AuthenticationBloc bloc) => bloc.state.user.lastName,
    );

    return Text('User: $userFirstName $userLastName');
  }
}
