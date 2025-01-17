import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/integration/bloc/integrations_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations_event.dart';
import 'package:triggo/app/features/integration/bloc/integrations_state.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/widgets/card.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/models/integrations/notion.integration.model.dart';
import 'package:triggo/models/integrations/openAI.integration.model.dart';

class AutomationSelectIntegrationsAccountView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;
  final String triggerOrActionIdentifier;

  const AutomationSelectIntegrationsAccountView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
    required this.triggerOrActionIdentifier,
  });

  @override
  State<AutomationSelectIntegrationsAccountView> createState() =>
      _AutomationSelectIntegrationsAccountViewState();
}

class _AutomationSelectIntegrationsAccountViewState
    extends State<AutomationSelectIntegrationsAccountView> {
  late List<String> selectedIntegrations = [];

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
            onTap: (integrationId) {
              if (selectedIntegrations.contains(integrationId)) {
                setState(() {
                  selectedIntegrations.remove(integrationId);
                });
              } else {
                setState(() {
                  selectedIntegrations.add(integrationId);
                });
              }
            },
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
  final void Function(String) onTap;

  const _StateManager({
    required this.state,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
    required this.onTap,
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
        onTap: onTap,
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
  final void Function(String) onTap;

  const _IntegrationList({
    required this.integrations,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
    required this.onTap,
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
              onTap: onTap,
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
  final void Function(String) onTap;

  const _IntegrationListItem({
    required this.integration,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    switch (integration.name) {
      case IntegrationNames.discord:
        final discord = integration as DiscordIntegration;
        return _ItemWidget(
          id: discord.id,
          title: discord.displayName,
          description: "${discord.username} - ${discord.email}",
          avatarUri: discord.avatarUri,
          integrationSvg: "assets/icons/discord.svg",
          onTap: onTap,
        );

      case IntegrationNames.notion:
        final notion = integration as NotionIntegration;
        return _ItemWidget(
          id: notion.id,
          title: notion.workspaceName,
          description: notion.email,
          avatarUri: notion.avatarUri,
          integrationSvg: "assets/icons/notion.svg",
          onTap: onTap,
        );
      case IntegrationNames.openAI:
        final openAI = integration as OpenAIIntegration;
        return _ItemWidget(
          id: openAI.id,
          title: openAI.ownerName,
          description: openAI.ownerName,
          integrationSvg: "assets/icons/openai.svg",
          onTap: onTap,
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

class _ItemWidget extends StatefulWidget {
  final String id;
  final String title;
  final String description;
  final String? avatarUri;
  final String integrationSvg;
  final void Function(String) onTap;

  const _ItemWidget({
    super.key,
    required this.id,
    required this.title,
    required this.description,
    this.avatarUri,
    required this.integrationSvg,
    required this.onTap,
  });

  @override
  State<_ItemWidget> createState() => _ItemWidgetState();
}

class _ItemWidgetState extends State<_ItemWidget> {
  bool selected = false;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        widget.onTap(widget.id);
        setState(() {
          selected = !selected;
        });
      },
      child: TriggoCard(
        customWidget: Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Checkbox(
                materialTapTargetSize: MaterialTapTargetSize.shrinkWrap,
                value: selected,
                onChanged: (_) {
                  widget.onTap(widget.id);
                  setState(() {
                    selected = !selected;
                  });
                }),
            SizedBox(width: 10),
            Stack(
              children: [
                if (widget.avatarUri == null)
                  CircleAvatar(
                    backgroundColor: Color(0xFF10a37f),
                    radius: 25,
                    child: SvgPicture.asset(
                      widget.integrationSvg,
                      width: 30,
                      height: 30,
                      colorFilter: ColorFilter.mode(
                        Colors.white,
                        BlendMode.srcIn,
                      ),
                    ),
                  ),
                if (widget.avatarUri != null)
                  CircleAvatar(
                    backgroundImage: NetworkImage(widget.avatarUri!),
                    radius: 25,
                  ),
                if (widget.avatarUri != null)
                  Positioned(
                    bottom: 0,
                    right: 0,
                    child: CircleAvatar(
                      radius: 10,
                      backgroundColor: Color(0xFF5865F2),
                      child: SvgPicture.asset(
                        widget.integrationSvg,
                        width: 15,
                        height: 15,
                        colorFilter: ColorFilter.mode(
                          Colors.white,
                          BlendMode.srcIn,
                        ),
                      ),
                    ),
                  ),
              ],
            ),
            SizedBox(width: 10),
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    widget.title,
                    style: Theme.of(context).textTheme.labelLarge?.copyWith(
                          height: 1.1,
                          fontSize: 24,
                          fontWeight: FontWeight.w700,
                        ),
                    overflow: TextOverflow.ellipsis,
                  ),
                  Text(
                    widget.description,
                    style: Theme.of(context).textTheme.labelMedium?.copyWith(
                          fontSize: 12,
                          fontWeight: FontWeight.w400,
                        ),
                    overflow: TextOverflow.ellipsis,
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
