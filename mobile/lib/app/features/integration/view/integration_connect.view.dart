import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/integration/widgets/integration.widget.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';

class IntegrationAvailableView extends StatefulWidget {
  final AutomationChoiceEnum? type;
  final List<AvailableIntegration>? integrationsNames;
  final int? indexOfTheTriggerOrAction;

  const IntegrationAvailableView({
    super.key,
    this.type,
    this.integrationsNames,
    this.indexOfTheTriggerOrAction,
  });

  @override
  State<IntegrationAvailableView> createState() =>
      _IntegrationAvailableViewState();
}

class _IntegrationAvailableViewState extends State<IntegrationAvailableView> {
  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    final Future<List<AvailableIntegration>> integrationsNames =
        widget.integrationsNames != null
            ? Future.value(widget.integrationsNames)
            : integrationMediator.getIntegrations();
    return BaseScaffold(
      title:
          widget.type != null ? 'Select an Integration' : 'Connect Integration',
      body: _IntegrationNamesContainer(
        integrations: integrationsNames,
        type: widget.type,
        indexOfTheTriggerOrAction: widget.indexOfTheTriggerOrAction,
      ),
      getBack: true,
    );
  }
}

class _IntegrationNamesContainer extends StatelessWidget {
  final Future<List<AvailableIntegration>> integrations;
  final AutomationChoiceEnum? type;
  final int? indexOfTheTriggerOrAction;

  const _IntegrationNamesContainer({
    required this.integrations,
    this.type,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(12.0),
        color: Colors.grey[200],
      ),
      child: Column(
        children: [
          _IntegrationList(
            integrations: integrations,
            type: type,
            indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
          ),
        ],
      ),
    );
  }
}

class _IntegrationList extends StatelessWidget {
  final Future<List<AvailableIntegration>> integrations;
  final AutomationChoiceEnum? type;
  final int? indexOfTheTriggerOrAction;

  const _IntegrationList({
    required this.integrations,
    this.type,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<AvailableIntegration>>(
      future: integrations,
      builder: (context, snapshot) {
        return IntegrationListView(
          snapshot: snapshot,
          type: type,
          indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
        );
      },
    );
  }
}

class IntegrationListView extends StatelessWidget {
  final AsyncSnapshot<List<AvailableIntegration>> snapshot;
  final AutomationChoiceEnum? type;
  final int? indexOfTheTriggerOrAction;

  const IntegrationListView({
    super.key,
    required this.snapshot,
    this.type,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    if (snapshot.connectionState == ConnectionState.waiting) {
      return CircularProgressIndicator();
    } else if (snapshot.hasError) {
      return _ErrorView(error: snapshot.error!);
    } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
      return const _NoDataView();
    } else {
      return _IntegrationListViewContent(
        integrations: snapshot.data!,
        type: type,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
      );
    }
  }
}

class _ErrorView extends StatelessWidget {
  final Object error;

  const _ErrorView({required this.error});

  @override
  Widget build(BuildContext context) {
    return Expanded(
        child: Center(
      child: Text('Error: $error',
          textAlign: TextAlign.center,
          style: Theme.of(context).textTheme.titleMedium),
    ));
  }
}

class _NoDataView extends StatelessWidget {
  const _NoDataView();

  @override
  Widget build(BuildContext context) {
    return Expanded(
        child: Center(
      child: Text('No data available',
          textAlign: TextAlign.center,
          style: Theme.of(context).textTheme.titleMedium),
    ));
  }
}

class _IntegrationListViewContent extends StatelessWidget {
  final List<AvailableIntegration> integrations;
  final AutomationChoiceEnum? type;
  final int? indexOfTheTriggerOrAction;

  const _IntegrationListViewContent({
    required this.integrations,
    this.type,
    this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: ListView.builder(
        itemCount: integrations.length,
        itemBuilder: (context, index) {
          final integration = integrations[index];
          return IntegrationListItemWidget(
            integration: integration,
            type: type,
            indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
          );
        },
      ),
    );
  }
}
