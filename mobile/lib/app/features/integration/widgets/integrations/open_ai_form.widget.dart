import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/integration/bloc/integrations/openAI/open_ai_bloc.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class OpenAIIntegrationForm extends StatelessWidget {
  const OpenAIIntegrationForm({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocListener<OpenAIIntegrationBloc, OpenAIIntegrationState>(
      listener: _listener,
      child: Align(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              _Label(),
              const SizedBox(height: 12),
              _ApiTokenInput(),
              const SizedBox(height: 12),
              _AdminApiTokenInput(),
              const SizedBox(height: 12),
              _LinkAccountButton(),
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
      'Enter your OpenAI keys',
      style: Theme.of(context).textTheme.titleLarge,
    );
  }
}

class _ApiTokenInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      placeholder: 'API Key',
      onChanged: (apiKey) {
        context.read<OpenAIIntegrationBloc>().add(OpenAIAPIKeyChanged(apiKey));
      },
    );
  }
}

class _AdminApiTokenInput extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      placeholder: 'Admin API Key',
      onChanged: (adminApiKey) {
        context
            .read<OpenAIIntegrationBloc>()
            .add(OpenAIAdminAPIKeyChanged(adminApiKey));
      },
    );
  }
}

class _LinkAccountButton extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final isInProgressOrSuccess = context.select(
      (OpenAIIntegrationBloc bloc) => bloc.state.status.isInProgressOrSuccess,
    );

    if (isInProgressOrSuccess) return const CircularProgressIndicator();

    final isValid =
        context.select((OpenAIIntegrationBloc bloc) => bloc.state.isValid);

    return SizedBox(
      width: double.infinity,
      child: TriggoButton(
          text: 'Link Account',
          onPressed: isValid
              ? () {
                  context
                      .read<OpenAIIntegrationBloc>()
                      .add(const OpenAISubmitted());
                }
              : null),
    );
  }
}

void _onLoginSuccess(BuildContext context, OpenAIIntegrationState state) {
  List<String> tokens = [state.apiToken.value, state.adminApiToken.value];
  Navigator.pop(context, tokens);
}

void _listener(BuildContext context, OpenAIIntegrationState state) {
  if (state.status.isFailure) {
    ScaffoldMessenger.of(context)
      ..hideCurrentSnackBar()
      ..showSnackBar(
        SnackBar(
            content: const Text('Link Failure'),
            backgroundColor: Theme.of(context).colorScheme.onError),
      );
    context.read<OpenAIIntegrationBloc>().add(const OpenAIReset());
    return;
  }
  if (state.status.isSuccess) {
    _onLoginSuccess(context, state);
  }
}
