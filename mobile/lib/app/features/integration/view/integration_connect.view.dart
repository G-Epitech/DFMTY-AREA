import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/integration/widgets/integrations/integration.widget.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';

class IntegrationConnectView extends StatefulWidget {
  const IntegrationConnectView({super.key});

  @override
  State<IntegrationConnectView> createState() => _IntegrationConnectViewState();
}

class _IntegrationConnectViewState extends State<IntegrationConnectView> {
  @override
  Widget build(BuildContext context) {
    final IntegrationMediator integrationMediator =
        RepositoryProvider.of<IntegrationMediator>(context);
    final Future<List<AvailableIntegration>> integrationsNames =
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

  final Future<List<AvailableIntegration>> integrations;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(12.0),
        color: Colors.grey[200],
      ),
      child: Column(
        children: [
          _IntegrationList(integrations: integrations),
        ],
      ),
    );
  }
}

class _IntegrationList extends StatelessWidget {
  const _IntegrationList({required this.integrations});

  final Future<List<AvailableIntegration>> integrations;

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<AvailableIntegration>>(
      future: integrations,
      builder: (context, snapshot) {
        return IntegrationListView(snapshot: snapshot);
      },
    );
  }
}

class IntegrationListView extends StatelessWidget {
  final AsyncSnapshot<List<AvailableIntegration>> snapshot;

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
      child: Text('No connected integration',
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _IntegrationListViewContent extends StatelessWidget {
  final List<AvailableIntegration> integrations;

  const _IntegrationListViewContent({required this.integrations});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: ListView.builder(
        itemCount: integrations.length,
        itemBuilder: (context, index) {
          final integration = integrations[index];
          return IntegrationListItemWidget(integration: integration);
        },
      ),
    );
  }
}
