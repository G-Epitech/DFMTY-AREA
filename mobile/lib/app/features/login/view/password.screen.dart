import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/login/bloc/login_bloc.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class PasswordInputScreen extends StatelessWidget {
  const PasswordInputScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: BlocListener<LoginBloc, LoginState>(
          listener: _listener,
          child: Column(
            children: [
              _PasswordLabel(),
              const SizedBox(height: 20),
              _PasswordInput(),
              const SizedBox(height: 20),
              _LoginButton(),
            ],
          ),
        ),
      ),
    );
  }
}

class _PasswordLabel extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Text(
      'Password',
      style: Theme.of(context).textTheme.titleMedium,
    );
  }
}

class _PasswordInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final displayError = context.select(
      (LoginBloc bloc) => bloc.state.password.displayError,
    );

    return TriggoInput(
      placeholder: 'Password',
      obscureText: true,
      onChanged: (password) {
        context.read<LoginBloc>().add(LoginPasswordChanged(password));
      },
    );
  }
}

class _LoginButton extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final isInProgressOrSuccess = context.select(
      (LoginBloc bloc) => bloc.state.status.isInProgressOrSuccess,
    );

    if (isInProgressOrSuccess) return const CircularProgressIndicator();

    final isValid = context.select((LoginBloc bloc) => bloc.state.isValid);

    return SizedBox(
      width: double.infinity,
      child: TriggoButton(
          text: 'Login',
          onPressed: isValid
              ? () {
                  context.read<LoginBloc>().add(const LoginSubmitted());
                }
              : null),
    );
  }
}

void _listener(BuildContext context, LoginState state) {
  if (state.status.isFailure) {
    ScaffoldMessenger.of(context)
      ..hideCurrentSnackBar()
      ..showSnackBar(
        SnackBar(
            content: const Text('Authentication Failure'),
            backgroundColor: Theme.of(context).colorScheme.onError),
      );
    context.read<LoginBloc>().add(const LoginReset());
    return;
  }
  if (state.status.isSuccess) {
    Navigator.of(context, rootNavigator: true).pushNamedAndRemoveUntil(
      RoutesNames.home,
      (route) => false,
    );
  }
}
