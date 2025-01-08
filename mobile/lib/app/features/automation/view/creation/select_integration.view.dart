import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';

class AutomationCreationSelectIntegrationView extends StatefulWidget {
  final AutomationChoiceEnum type;

  const AutomationCreationSelectIntegrationView({
    super.key,
    required this.type,
  });

  @override
  State<AutomationCreationSelectIntegrationView> createState() =>
      _AutomationCreationSelectIntegrationViewState();
}

class _AutomationCreationSelectIntegrationViewState
    extends State<AutomationCreationSelectIntegrationView> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: 'Select an integration',
      getBack: true,
      body: Padding(
        padding: const EdgeInsets.all(4.0),
        child: _List(),
      ),
    );
  }
}

class _List extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final AutomationMediator mediator =
        RepositoryProvider.of<AutomationMediator>(context);
    return ListView(children: []);
  }
}
