import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integrations/integration.names.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class IntegrationScreen extends StatefulWidget {
  const IntegrationScreen({super.key});

  @override
  State<IntegrationScreen> createState() => _IntegrationScreenState();
}

class _IntegrationScreenState extends State<IntegrationScreen> {
  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    final Future<List<Integration>> integrations =
        integrationMediator.getUserIntegrations();

    return BaseScaffold(
      title: 'Integrations',
      body: _IntegrationContainer(integrations: integrations),
    );
  }
}

class _IntegrationContainer extends StatelessWidget {
  const _IntegrationContainer({
    required this.integrations,
  });

  final Future<List<Integration>> integrations;

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

  final Future<List<Integration>> integrations;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<Integration>>(
      future: integrations,
      builder: (context, snapshot) {
        return _IntegrationListView(snapshot: snapshot);
      },
    );
  }
}

class _IntegrationListView extends StatelessWidget {
  final AsyncSnapshot<List<Integration>> snapshot;

  const _IntegrationListView({required this.snapshot});

  @override
  Widget build(BuildContext context) {
    if (snapshot.connectionState == ConnectionState.waiting) {
      return Center(child: CircularProgressIndicator());
    } else if (snapshot.hasError) {
      return _ErrorView(error: snapshot.error!);
    } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
      return const _NoDataView();
    } else {
      return _IntegrationListViewContent(integrations: snapshot.data!);
    }
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
      child: Text('No connected integrations',
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _IntegrationListViewContent extends StatelessWidget {
  final List<Integration> integrations;

  const _IntegrationListViewContent({required this.integrations});

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

class _IntegrationConnectionButton extends StatelessWidget {
  const _IntegrationConnectionButton();

  @override
  Widget build(BuildContext context) {
    return TriggoButton(
      text: 'New integration',
      onPressed: () {
        Navigator.pushNamed(context, RoutesNames.connectIntegration);
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
