import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/login/routes/custom.router.dart';
import 'package:triggo/app/features/login/view/password.screen.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class EmailInputScreen extends StatelessWidget {
  const EmailInputScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () {
            Navigator.of(context, rootNavigator: true).pop();
          },
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            _EmailLabel(),
            const SizedBox(height: 20),
            _EmailInput(),
            const SizedBox(height: 20),
            _NextButton(),
          ],
        ),
      ),
    );
  }
}

class _EmailLabel extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Text(
      'Email',
      style: Theme.of(context).textTheme.titleMedium,
    );
  }
}

class _EmailInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final displayError = context.select(
      (LoginBloc bloc) => bloc.state.email.displayError,
    );

    return TriggoInput(
      placeholder: 'Email',
      onChanged: (email) {
        context.read<LoginBloc>().add(LoginEmailChanged(email));
      },
    );
  }
}

class _NextButton extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final emailError = context.select(
      (LoginBloc bloc) => bloc.state.email.displayError,
    );
    final email = context.select(
      (LoginBloc bloc) => bloc.state.email.value,
    );

    return SizedBox(
      width: double.infinity,
      child: TriggoButton(
        text: 'Next',
        onPressed: emailError == null && email.isNotEmpty
            ? () {
                Navigator.of(context).push(
                  CustomLoginRouter(child: const PasswordInputScreen()),
                );
              }
            : null,
      ),
    );
  }
}
