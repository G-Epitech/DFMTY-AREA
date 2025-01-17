import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/singleton/parameters.view.dart';
import 'package:triggo/app/features/integration/bloc/integrations_bloc.dart';
import 'package:triggo/app/features/integration/bloc/integrations_event.dart';
import 'package:triggo/app/features/integration/bloc/integrations_state.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/features/integration/utils/automation_update_provider.dart';
import 'package:triggo/app/routes/custom.router.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/automation.model.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/models/integrations/notion.integration.model.dart';
import 'package:triggo/models/integrations/openAI.integration.model.dart';

class AutomationSelectIntegrationsAccountView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;
  final String triggerOrActionIdentifier;
  final Map<String, AutomationSchemaDependenciesProperty> dependencies;

  const AutomationSelectIntegrationsAccountView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
    required this.triggerOrActionIdentifier,
    required this.dependencies,
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
    final List<String> needIntegrations = widget.dependencies.keys.toList();
    final isRadio = _isRadio(widget.dependencies);
    return BlocProvider(
      create: (context) => IntegrationsBloc(
        integrationMediator,
      )..add(LoadIntegrations(integrationIdentifier: needIntegrations)),
      child: BaseScaffold(
        title: 'Integrations',
        getBack: true,
        body: BlocBuilder<IntegrationsBloc, IntegrationsState>(
            builder: (context, state) {
          return _StateManager(
            values: selectedIntegrations,
            state: state,
            type: widget.type,
            integrationIdentifier: widget.integrationIdentifier,
            indexOfTheTriggerOrAction: widget.indexOfTheTriggerOrAction,
            triggerOrActionIdentifier: widget.triggerOrActionIdentifier,
            dependencies: widget.dependencies,
            onTap: (integrationId) {
              if (isRadio) {
                setState(() {
                  selectedIntegrations = [integrationId];
                });
                return;
              } else {
                if (selectedIntegrations.contains(integrationId)) {
                  setState(() {
                    selectedIntegrations.remove(integrationId);
                  });
                } else {
                  setState(() {
                    selectedIntegrations.add(integrationId);
                  });
                }
              }
            },
          );
        }),
      ),
    );
  }
}

class _StateManager extends StatelessWidget {
  final List<String> values;
  final IntegrationsState state;
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;
  final String triggerOrActionIdentifier;
  final void Function(String) onTap;
  final Map<String, AutomationSchemaDependenciesProperty> dependencies;

  const _StateManager({
    required this.values,
    required this.state,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
    required this.triggerOrActionIdentifier,
    required this.onTap,
    required this.dependencies,
  });

  @override
  Widget build(BuildContext context) {
    if (state is IntegrationsLoading) {
      return const Center(child: CircularProgressIndicator());
    }
    if (state is IntegrationsError) {
      return _ErrorView(error: (state as IntegrationsError).message);
    }

    if (state is IntegrationsLoaded &&
        (state as IntegrationsLoaded).integrations.isNotEmpty) {
      return _IntegrationList(
        values: values,
        integrations: (state as IntegrationsLoaded).integrations,
        type: type,
        integrationIdentifier: integrationIdentifier,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
        triggerOrActionIdentifier: triggerOrActionIdentifier,
        onTap: onTap,
        dependencies: dependencies,
      );
    } else {
      return const _NoDataView();
    }
  }
}

class _IntegrationList extends StatelessWidget {
  final List<String> values;
  final List<Integration> integrations;
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;
  final String triggerOrActionIdentifier;
  final void Function(String) onTap;
  final Map<String, AutomationSchemaDependenciesProperty> dependencies;

  const _IntegrationList({
    required this.values,
    required this.integrations,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
    required this.triggerOrActionIdentifier,
    required this.onTap,
    required this.dependencies,
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
              isRadio: _isRadio(dependencies),
              selected: values,
            );
          },
        )),
        _OKButton(
          values: values,
          integrations: integrations,
          type: type,
          integrationIdentifier: integrationIdentifier,
          indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
          triggerOrActionIdentifier: triggerOrActionIdentifier,
          dependencies: dependencies,
        )
      ],
    );
  }
}

bool _isRadio(Map<String, AutomationSchemaDependenciesProperty> dependencies) {
  return dependencies.length == 1 &&
      dependencies.values.first.require == "Single" &&
      dependencies.values.first.optional == false;
}

class _OKButton extends StatelessWidget {
  final List<String> values;
  final List<Integration> integrations;
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final int indexOfTheTriggerOrAction;
  final String triggerOrActionIdentifier;
  final Map<String, AutomationSchemaDependenciesProperty> dependencies;

  const _OKButton({
    required this.values,
    required this.integrations,
    required this.type,
    required this.integrationIdentifier,
    required this.indexOfTheTriggerOrAction,
    required this.triggerOrActionIdentifier,
    required this.dependencies,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(top: 8.0),
      child: Row(
        children: [
          Expanded(
            child: TriggoButton(
              text: "OK",
              padding:
                  const EdgeInsets.symmetric(horizontal: 20.0, vertical: 12.0),
              onPressed: () {
                try {
                  _isValid();
                  automationUpdateDependencies(
                    context,
                    type,
                    values,
                    indexOfTheTriggerOrAction,
                  );
                  Navigator.push(
                      context,
                      customScreenBuilder(AutomationParametersView(
                        type: type,
                        integrationIdentifier: integrationIdentifier,
                        triggerOrActionIdentifier: triggerOrActionIdentifier,
                        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
                      )));
                } catch (e) {
                  final error = e.toString();

                  ScaffoldMessenger.of(context)
                    ..hideCurrentSnackBar()
                    ..showSnackBar(
                      SnackBar(
                          content: Text(error),
                          backgroundColor:
                              Theme.of(context).colorScheme.onError),
                    );
                }
              },
            ),
          ),
        ],
      ),
    );
  }

  void _isValid() {
    final Map<String, int> selectedIntegrations = {};

    for (final integration in integrations) {
      for (final value in values) {
        if (integration.id == value) {
          final integrationName = integration.name.toLowerCase();
          selectedIntegrations[integrationName] =
              (selectedIntegrations[integrationName] ?? 0) + 1;
        }
      }
    }

    if (dependencies.length != selectedIntegrations.length) {
      log("Dependencies length is not equal to selected integrations length");
      final List<String> missingIntegrations = [];
      for (final dependency in dependencies.entries) {
        final dependencyName = dependency.key.toLowerCase();
        if (selectedIntegrations[dependencyName] == null) {
          missingIntegrations.add(dependencyName);
        }
      }
      throw "You must select at least one of each of the following integrations: ${missingIntegrations.join(", ")}";
    }

    for (final dependency in dependencies.entries) {
      final dependencyName = dependency.key.toLowerCase();
      if (dependency.value.optional) {
        continue;
      }
      if (selectedIntegrations[dependencyName] == null) {
        log("Dependency $dependencyName is not met");
        throw "You must select $dependencyName";
      }
      if (dependency.value.require == "Single" &&
          selectedIntegrations[dependencyName] != 1) {
        log("Single dependency $dependencyName is not met");
        throw "You must select one $dependencyName";
      }
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
      child: Text('No accounts connected',
          textAlign: TextAlign.center,
          style: Theme.of(context).textTheme.titleMedium),
    );
  }
}

class _IntegrationListItem extends StatelessWidget {
  final Integration integration;
  final void Function(String) onTap;
  final List<String> selected;
  final bool isRadio;

  const _IntegrationListItem({
    required this.integration,
    required this.onTap,
    required this.selected,
    required this.isRadio,
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
          selected: selected,
          isRadio: isRadio,
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
          selected: selected,
          isRadio: isRadio,
        );
      case IntegrationNames.openAI:
        final openAI = integration as OpenAIIntegration;
        return _ItemWidget(
          id: openAI.id,
          title: openAI.ownerName,
          description: openAI.ownerName,
          integrationSvg: "assets/icons/openai.svg",
          onTap: onTap,
          selected: selected,
          isRadio: isRadio,
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
  final List<String> selected;
  final bool isRadio;

  const _ItemWidget({
    required this.id,
    required this.title,
    required this.description,
    this.avatarUri,
    required this.integrationSvg,
    required this.onTap,
    required this.selected,
    required this.isRadio,
  });

  @override
  State<_ItemWidget> createState() => _ItemWidgetState();
}

class _ItemWidgetState extends State<_ItemWidget> {
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      behavior: HitTestBehavior.opaque,
      onTap: () {
        setState(() {
          widget.onTap(widget.id);
        });
      },
      child: Container(
          margin: EdgeInsets.only(bottom: 8.0),
          padding: const EdgeInsets.symmetric(vertical: 16.0, horizontal: 16.0),
          decoration: BoxDecoration(
            color: Theme.of(context).colorScheme.surface,
            border: Border.all(
              color: widget.selected.contains(widget.id)
                  ? Theme.of(context).colorScheme.primary
                  : Colors.transparent,
            ),
            borderRadius: BorderRadius.circular(8.0),
          ),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              if (widget.isRadio)
                Container(
                  height: 22,
                  width: 22,
                  decoration: BoxDecoration(
                    shape: BoxShape.circle,
                    border: Border.all(
                      color: widget.selected.contains(widget.id)
                          ? Theme.of(context).colorScheme.primary
                          : Colors.grey.shade400,
                      width: 2,
                    ),
                  ),
                  child: widget.selected.contains(widget.id)
                      ? Center(
                          child: Container(
                            height: 12,
                            width: 12,
                            decoration: BoxDecoration(
                              shape: BoxShape.circle,
                              color: Theme.of(context).colorScheme.primary,
                            ),
                          ),
                        )
                      : null,
                ),
              if (!widget.isRadio)
                SizedBox(
                  width: 20,
                  child: Checkbox(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.all(
                          Radius.circular(5.0),
                        ),
                      ),
                      materialTapTargetSize: MaterialTapTargetSize.shrinkWrap,
                      value: widget.selected.contains(widget.id),
                      onChanged: (_) {
                        widget.onTap(widget.id);
                      }),
                ),
              SizedBox(width: 16),
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
              SizedBox(width: 15),
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
          )),
    );
  }
}
