import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/login/bloc/login_bloc.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';
import 'package:triggo/app/widgets/navigation_bar.triggo.dart';

class LoginForm extends StatelessWidget {
  const LoginForm({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocListener<LoginBloc, LoginState>(
      listener: _listener,
      child: Align(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              _Label(),
              const SizedBox(height: 12),
              _EmailInput(),
              const SizedBox(height: 12),
              _PasswordInput(),
              const SizedBox(height: 12),
              _LoginButton(),
            ],
          ),
        ),
      ),
    );
  }
}

class _Label extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Text(
      'Welcome Back',
      style: Theme.of(context).textTheme.titleLarge,
    );
  }
}

class _EmailInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    /*final displayError = context.select(
      (LoginBloc bloc) => bloc.state.email.displayError,
    );*/

    return TriggoInput(
      placeholder: 'Email',
      onChanged: (email) {
        context.read<LoginBloc>().add(LoginEmailChanged(email));
      },
      maxLines: 1,
    );
  }
}

class _PasswordInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    /*final displayError = context.select(
      (LoginBloc bloc) => bloc.state.password.displayError,
    );*/

    return TriggoInput(
      placeholder: 'Password',
      obscureText: true,
      onChanged: (password) {
        context.read<LoginBloc>().add(LoginPasswordChanged(password));
      },
      maxLines: 1,
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

void _onLoginSuccess(BuildContext context) {
  currentRouteNotifier.value = RoutesNames.home;
  Navigator.pushNamedAndRemoveUntil(
    context,
    RoutesNames.home,
    (route) => false,
  );
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
    _onLoginSuccess(context);
  }
}
