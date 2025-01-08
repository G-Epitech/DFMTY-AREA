import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/register/bloc/register_bloc.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class RegisterForm extends StatelessWidget {
  const RegisterForm({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocListener<RegisterBloc, RegisterState>(
      listener: _listener,
      child: Align(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              _Label(),
              const SizedBox(height: 12),
              _FirstNameInput(),
              const SizedBox(height: 12),
              _LastNameInput(),
              const SizedBox(height: 12),
              _EmailInput(),
              const SizedBox(height: 12),
              _PasswordInput(),
              const SizedBox(height: 12),
              _RegisterButton(),
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
      'Welcome',
      style: Theme.of(context).textTheme.titleLarge,
    );
  }
}

class _FirstNameInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    /*final displayError = context.select(
      (RegisterBloc bloc) => bloc.state.firstName.displayError,
    );*/

    return TriggoInput(
      placeholder: 'First Name',
      onChanged: (firstName) {
        context.read<RegisterBloc>().add(RegisterFirstNameChanged(firstName));
      },
    );
  }
}

class _LastNameInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    /*final displayError = context.select(
      (RegisterBloc bloc) => bloc.state.lastName.displayError,
    );*/

    return TriggoInput(
      placeholder: 'Last Name',
      onChanged: (lastName) {
        context.read<RegisterBloc>().add(RegisterLastNameChanged(lastName));
      },
    );
  }
}

class _EmailInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    /*final displayError = context.select(
      (RegisterBloc bloc) => bloc.state.email.displayError,
    );*/

    return TriggoInput(
      placeholder: 'Email',
      onChanged: (email) {
        context.read<RegisterBloc>().add(RegisterEmailChanged(email));
      },
    );
  }
}

class _PasswordInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    /*final displayError = context.select(
      (RegisterBloc bloc) => bloc.state.password.displayError,
    );*/

    return TriggoInput(
      placeholder: 'Password',
      obscureText: true,
      onChanged: (password) {
        context.read<RegisterBloc>().add(RegisterPasswordChanged(password));
      },
    );
  }
}

class _RegisterButton extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final isInProgressOrSuccess = context.select(
      (RegisterBloc bloc) => bloc.state.status.isInProgressOrSuccess,
    );

    if (isInProgressOrSuccess) return const CircularProgressIndicator();

    final isValid = context.select((RegisterBloc bloc) => bloc.state.isValid);

    return SizedBox(
      width: double.infinity,
      child: TriggoButton(
          text: 'Register',
          onPressed: isValid
              ? () {
                  context.read<RegisterBloc>().add(const RegisterSubmitted());
                }
              : null),
    );
  }
}

void _listener(BuildContext context, RegisterState state) {
  if (state.status.isFailure) {
    ScaffoldMessenger.of(context)
      ..hideCurrentSnackBar()
      ..showSnackBar(
        SnackBar(
            content: const Text('Authentication Failure'),
            backgroundColor: Theme.of(context).colorScheme.onError),
      );
    context.read<RegisterBloc>().add(const RegisterReset());
    return;
  }
  if (state.status.isSuccess) {
    Navigator.of(context, rootNavigator: true).pushNamedAndRemoveUntil(
      RoutesNames.home,
      (route) => false,
    );
  }
}
