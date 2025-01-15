import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/integration/bloc/integrations_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations_event.dart';
import 'package:triggo/app/features/integration/bloc/integrations_state.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/features/integration/widgets/integrations/notion.integrations.widget.dart';
import 'package:triggo/app/features/integration/widgets/integrations/open_ai.integrations.widget.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/models/integrations/notion.integration.model.dart';
import 'package:triggo/models/integrations/openAI.integration.model.dart';

class AutomationCreationSelectIntegrationsAccountView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;

  const AutomationCreationSelectIntegrationsAccountView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  State<AutomationCreationSelectIntegrationsAccountView> createState() =>
      _AutomationCreationSelectIntegrationsAccountViewState();
}

class _AutomationCreationSelectIntegrationsAccountViewState
    extends State<AutomationCreationSelectIntegrationsAccountView> {
  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    return BlocProvider(
      create: (context) => IntegrationsBloc(
        integrationMediator,
      )..add(LoadIntegrations(
          integrationIdentifier: widget.integrationIdentifier)),
      child: BaseScaffold(
        title: 'Integrations',
        getBack: true,
        body: BlocBuilder<IntegrationsBloc, IntegrationsState>(
            builder: (context, state) {
          return _StateManager(
            state: state,
            type: widget.type,
            integrationIdentifier: widget.integrationIdentifier,
            indexOfTheTriggerOrAction: widget.indexOfTheTriggerOrAction,
          );
        }),
      ),
    );
  }
}

class _StateManager extends StatelessWidget {
  final IntegrationsState state;
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;

  const _StateManager({
    required this.state,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    if (state is IntegrationsLoading) {
      return const Center(child: CircularProgressIndicator());
    } else if (state is IntegrationsLoaded &&
        (state as IntegrationsLoaded).integrations.isNotEmpty) {
      return _IntegrationList(
        integrations: (state as IntegrationsLoaded).integrations,
        type: type,
        integrationIdentifier: integrationIdentifier,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
      );
    } else if (state is IntegrationsError) {
      return _ErrorView(error: (state as IntegrationsError).message);
    } else {
      return const _NoDataView();
    }
  }
}

class _IntegrationList extends StatelessWidget {
  final List<Integration> integrations;
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;

  const _IntegrationList({
    required this.integrations,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Expanded(
            child: ListView.builder(
          itemCount: integrations.length,
          itemBuilder: (context, index) {
            final integration = integrations[index];
            return _IntegrationListItem(
              integration: integration,
              type: type,
              integrationIdentifier: integrationIdentifier,
              indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
            );
          },
        )),
      ],
    );
  }
}

class _ErrorView extends StatelessWidget {
  final Object error;

  const _ErrorView({required this.error});

  @override
  Widget build(BuildContext context) {
    return Text('Error: $error');
  }
}

class _NoDataView extends StatelessWidget {
  const _NoDataView();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Text('No accounts connected',
          textAlign: TextAlign.center,
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _IntegrationListItem extends StatelessWidget {
  final Integration integration;
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;

  const _IntegrationListItem({
    required this.integration,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    switch (integration.name) {
      case IntegrationNames.discord:
        return DiscordIntegrationListItemWidget(
          integration: integration as DiscordIntegration,
          type: type,
          integrationIdentifier: integrationIdentifier,
          indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
        );
      case IntegrationNames.notion:
        return NotionIntegrationListItemWidget(
          integration: integration as NotionIntegration,
          type: type,
          integrationIdentifier: integrationIdentifier,
          indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
        );
      case IntegrationNames.openAI:
        return OpenAIIntegrationListItemWidget(
          integration: integration as OpenAIIntegration,
          type: type,
          integrationIdentifier: integrationIdentifier,
          indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
        );
      default:
        return _DefaultIntegrationListItem(integration: integration);
    }
  }
}

class _DefaultIntegrationListItem extends StatelessWidget {
  final Integration integration;

  const _DefaultIntegrationListItem({required this.integration});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: Text(integration.name),
    );
  }
}
