import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integrations/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';

class AutomationCreationAddView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationName;

  const AutomationCreationAddView({
    super.key,
    required this.type,
    required this.integrationName,
  });

  @override
  State<AutomationCreationAddView> createState() =>
      _AutomationCreationAddViewState();
}

class _AutomationCreationAddViewState extends State<AutomationCreationAddView> {
  @override
  Widget build(BuildContext context) {
    log('Integration name: ${widget.integrationName}');
    return BaseScaffold(
      title: 'Add an integration',
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
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    final Future<List<AvailableIntegration>> integrationsNames =
        integrationMediator.getIntegrations();
    return ListView(children: []);
  }
}
