import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations/openAI/open_ai_bloc.dart';
import 'package:triggo/app/features/integration/widgets/integrations/open_ai_form.widget.dart';
import 'package:triggo/mediator/integrations/openAI.mediator.dart';

class OpenAIIntegrationView extends StatelessWidget {
  const OpenAIIntegrationView({super.key});

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
          create: (context) => OpenAIIntegrationBloc(
            openAIMediator: context.read<OpenAIMediator>(),
          ),
          child: const OpenAIIntegrationForm(),
        ),
      ),
    );
  }
}
