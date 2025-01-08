import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/register/bloc/register_bloc.dart';
import 'package:triggo/app/features/register/widgets/form.widget.dart';
import 'package:triggo/mediator/authentication.mediator.dart';

class RegisterView extends StatelessWidget {
  const RegisterView({super.key});

  static Route<void> route() {
    return MaterialPageRoute<void>(builder: (_) => const RegisterView());
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => Navigator.of(context).pop(),
        ),
      ),
      extendBodyBehindAppBar: true,
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: BlocProvider(
          create: (context) => RegisterBloc(
            authenticationMediator: context.read<AuthenticationMediator>(),
          ),
          child: const RegisterForm(),
        ),
      ),
    );
  }
}
