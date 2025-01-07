import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations_event.dart';
import 'package:triggo/app/features/integration/bloc/integrations_state.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class IntegrationsView extends StatefulWidget {
  const IntegrationsView({super.key});

  @override
  State<IntegrationsView> createState() => _IntegrationsViewState();
}

class _IntegrationsViewState extends State<IntegrationsView> {
  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    return BlocProvider(
      create: (context) => IntegrationsBloc(
        integrationMediator,
      )..add(LoadIntegrations()),
      child: BaseScaffold(
        title: 'Integrations',
        body: BlocBuilder<IntegrationsBloc, IntegrationsState>(
            builder: (context, state) {
          return _StateManager(state: state);
        }),
      ),
    );
  }
}

class _StateManager extends StatelessWidget {
  final IntegrationsState state;

  const _StateManager({required this.state});

  @override
  Widget build(BuildContext context) {
    if (state is IntegrationsLoading) {
      return const Center(child: CircularProgressIndicator());
    } else if (state is IntegrationsLoaded) {
      return _IntegrationContainer(
          integrations: (state as IntegrationsLoaded).integrations);
    } else if (state is IntegrationsError) {
      return _ErrorView(error: (state as IntegrationsError).message);
    } else {
      return const _NoDataView();
    }
  }
}

class _IntegrationContainer extends StatelessWidget {
  const _IntegrationContainer({
    required this.integrations,
  });

  final List<Integration> integrations;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          children: [
            Expanded(
              child: Padding(
                padding:
                    const EdgeInsets.symmetric(vertical: 8.0, horizontal: 4.0),
                child: _IntegrationConnectionButton(),
              ),
            ),
          ],
        ),
        Expanded(child: _IntegrationList(integrations: integrations)),
      ],
    );
  }
}

class _IntegrationList extends StatelessWidget {
  const _IntegrationList({required this.integrations});

  final List<Integration> integrations;

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: integrations.length,
      itemBuilder: (context, index) {
        final integration = integrations[index];
        return _IntegrationListItem(integration: integration);
      },
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
      child: Text('No connected integration',
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _IntegrationConnectionButton extends StatelessWidget {
  const _IntegrationConnectionButton();

  @override
  Widget build(BuildContext context) {
    return TriggoButton(
      text: 'New integration',
      onPressed: () {
        Navigator.pushNamed(context, RoutesNames.connectIntegration).then((_) {
          context.read<IntegrationsBloc>().add(ReloadIntegrations());
        });
      },
    );
  }
}

class _IntegrationListItem extends StatelessWidget {
  final Integration integration;

  const _IntegrationListItem({required this.integration});

  @override
  Widget build(BuildContext context) {
    switch (integration.name) {
      case IntegrationNames.discord:
        return DiscordIntegrationListItemWidget(
            integration: integration as DiscordIntegration);
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
