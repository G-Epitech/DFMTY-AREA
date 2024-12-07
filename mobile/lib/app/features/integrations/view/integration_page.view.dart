import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integrations/integration.names.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/banner.triggo.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

class IntegrationPage extends StatefulWidget {
  const IntegrationPage({super.key});

  @override
  State<IntegrationPage> createState() => _IntegrationPageState();
}

class _IntegrationPageState extends State<IntegrationPage> {
  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    final Future<List<Integration>> integrations =
        integrationMediator.getIntegrations();

    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: SafeArea(
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 8.0),
            child: Column(
              children: [
                TriggoBanner(),
                const SizedBox(height: 16.0),
                _PageTitle(),
                const SizedBox(height: 4.0),
                _IntegrationContainer(integrations: integrations),
              ],
            ),
          ),
        ),
      ),
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
    return Expanded(
      child: Container(
        padding: const EdgeInsets.all(8.0),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(12.0),
          color: Colors.grey[200],
        ),
        child: Column(
          children: [
            Row(
              children: [
                Expanded(
                  child: Padding(
                    padding: const EdgeInsets.symmetric(
                        vertical: 8.0, horizontal: 4.0),
                    child: _IntegrationConnectionButton(),
                  ),
                ),
              ],
            ),
            Expanded(child: _IntegrationList(integrations: integrations)),
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
    return Align(
      alignment: Alignment.centerLeft,
      child: Text(
        'Integrations',
        style: Theme.of(context).textTheme.titleLarge,
      ),
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
        return IntegrationListView(snapshot: snapshot);
      },
    );
  }
}

class IntegrationListView extends StatelessWidget {
  final AsyncSnapshot<List<Integration>> snapshot;

  const IntegrationListView({required this.snapshot, super.key});

  @override
  Widget build(BuildContext context) {
    if (snapshot.connectionState == ConnectionState.waiting) {
      return CircularProgressIndicator();
    } else if (snapshot.hasError) {
      return _ErrorView(error: snapshot.error);
    } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
      return const _NoDataView();
    } else {
      return _IntegrationListViewContent(integrations: snapshot.data!);
    }
  }
}

class _ErrorView extends StatelessWidget {
  final Object? error;

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
    return Expanded(
      child: ListView.builder(
        itemCount: 10,
        itemBuilder: (context, index) {
          final integration = integrations[0];
          return _IntegrationListItem(integration: integration);
        },
      ),
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
        Navigator.pushNamedAndRemoveUntil(
            context, RoutesNames.connectIntegration, (route) => false);
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
