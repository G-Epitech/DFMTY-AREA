import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations/openAI/open_ai_bloc.dart';
import 'package:triggo/app/features/integration/widgets/integrations/open_ai_form.widget.dart';
import 'package:triggo/mediator/integration.mediator.dart';

class OpenAIIntegrationView extends StatelessWidget {
  const OpenAIIntegrationView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      extendBodyBehindAppBar: true,
      body: Stack(
        children: [
          Padding(
            padding: EdgeInsets.only(
              top: kToolbarHeight + MediaQuery.of(context).padding.top,
              left: 12,
              right: 12,
              bottom: 12,
            ),
            child: BlocProvider(
              create: (context) => OpenAIIntegrationBloc(
                openAIMediator: context.read<IntegrationMediator>().openAI,
              ),
              child: const OpenAIIntegrationForm(),
            ),
          ),
          Positioned(
            top: 0,
            left: 0,
            right: 0,
            child: Container(
              color: Colors.white,
              padding: EdgeInsets.only(
                top: MediaQuery.of(context).padding.top,
                left: 8,
                right: 8,
              ),
              child: Row(
                children: [
                  IconButton(
                    icon: const Icon(Icons.arrow_back),
                    onPressed: () => Navigator.of(context).pop(),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
