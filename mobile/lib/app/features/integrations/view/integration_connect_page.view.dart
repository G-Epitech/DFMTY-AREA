import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integrations/widgets/integrations/discord_connect.integrations.widget.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';

class ConnectIntegrationScreen extends StatefulWidget {
  const ConnectIntegrationScreen({super.key});

  @override
  State<ConnectIntegrationScreen> createState() =>
      _ConnectIntegrationScreenState();
}

class _ConnectIntegrationScreenState extends State<ConnectIntegrationScreen> {
  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    final Future<List<String>> integrationsNames =
        integrationMediator.getIntegrationNames();

    return BaseScaffold(
      title: 'Connect Integration',
      body: _IntegrationNamesContainer(integrations: integrationsNames),
      getBack: true,
    );
  }
}

class _IntegrationNamesContainer extends StatelessWidget {
  const _IntegrationNamesContainer({
    required this.integrations,
  });

  final Future<List<String>> integrations;

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Container(
        padding: const EdgeInsets.all(8.0),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(12.0),
          color: Colors.grey[200],
        ),
        child: Column(
          children: [
            _IntegrationList(integrations: integrations),
          ],
        ),
      ),
    );
  }
}

class _PageTitle extends StatelessWidget {
  const _PageTitle();

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        GestureDetector(
          onTap: () {
            Navigator.of(context).pop();
          },
          child: Icon(
            Icons.arrow_back,
            size: 26.0,
            weight: 2.0,
          ),
        ),
        SizedBox(width: 10.0),
        Text(
          'Connect',
          style: Theme.of(context).textTheme.titleLarge,
        ),
      ],
    );
  }
}

class _IntegrationList extends StatelessWidget {
  const _IntegrationList({required this.integrations});

  final Future<List<String>> integrations;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<String>>(
      future: integrations,
      builder: (context, snapshot) {
        return IntegrationListView(snapshot: snapshot);
      },
    );
  }
}

class IntegrationListView extends StatelessWidget {
  final AsyncSnapshot<List<String>> snapshot;

  const IntegrationListView({required this.snapshot, super.key});

  @override
  Widget build(BuildContext context) {
    if (snapshot.connectionState == ConnectionState.waiting) {
      return CircularProgressIndicator();
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
  final List<String> integrations;

  const _IntegrationListViewContent({required this.integrations});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: ListView.builder(
        itemCount: integrations.length,
        itemBuilder: (context, index) {
          final integration = integrations[index];
          return _IntegrationListItem(integration: integration);
        },
      ),
    );
  }
}

class _IntegrationListItem extends StatelessWidget {
  final String integration;

  const _IntegrationListItem({required this.integration});

  @override
  Widget build(BuildContext context) {
    switch (integration) {
      case "Discord":
        return DiscordConnectIntegrationListItemWidget();
      default:
        return Text('Integration not found: $integration');
    }
  }
}
