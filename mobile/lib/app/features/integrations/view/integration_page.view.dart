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
                    child: IntegrationConnectionButton(),
                  ),
                ),
              ],
            ),
            Expanded(child: IntegrationList(integrations: integrations)),
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

class IntegrationList extends StatelessWidget {
  const IntegrationList({super.key, required this.integrations});

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
      return ErrorView(error: snapshot.error);
    } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
      return const NoDataView();
    } else {
      return IntegrationListViewContent(integrations: snapshot.data!);
    }
  }
}

class ErrorView extends StatelessWidget {
  final Object? error;

  const ErrorView({required this.error, super.key});

  @override
  Widget build(BuildContext context) {
    return Text('Error: $error');
  }
}

class NoDataView extends StatelessWidget {
  const NoDataView({super.key});

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Text('No connected integrations',
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class IntegrationListViewContent extends StatelessWidget {
  final List<Integration> integrations;

  const IntegrationListViewContent({required this.integrations, super.key});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: ListView.builder(
        itemCount: 10,
        itemBuilder: (context, index) {
          final integration = integrations[0];
          return IntegrationListItem(integration: integration);
        },
      ),
    );
  }
}

class IntegrationConnectionButton extends StatelessWidget {
  const IntegrationConnectionButton({
    super.key,
  });

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

class IntegrationListItem extends StatelessWidget {
  final Integration integration;

  const IntegrationListItem({required this.integration, super.key});

  @override
  Widget build(BuildContext context) {
    switch (integration.name) {
      case IntegrationNames.discord:
        return DiscordIntegrationListItemWidget(
            integration: integration as DiscordIntegration);
      default:
        return DefaultIntegrationListItem(integration: integration);
    }
  }
}

class DefaultIntegrationListItem extends StatelessWidget {
  final Integration integration;

  const DefaultIntegrationListItem({required this.integration, super.key});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: Text(integration.name),
    );
  }
}
