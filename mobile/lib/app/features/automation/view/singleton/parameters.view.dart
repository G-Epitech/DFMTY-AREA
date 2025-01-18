import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/view/singleton/input.view.dart';
import 'package:triggo/app/features/automation/view/singleton/settings.view.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/fonts/fonts.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationParametersView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final String triggerOrActionIdentifier;
  final int indexOfTheTriggerOrAction;
  final bool isEdit;

  const AutomationParametersView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
    required this.indexOfTheTriggerOrAction,
    this.isEdit = false,
  });

  @override
  State<AutomationParametersView> createState() =>
      _AutomationParametersViewState();
}

class _AutomationParametersViewState extends State<AutomationParametersView> {
  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    final Map<String, AutomationSchemaTriggerActionProperty> properties =
        automationMediator.getParameters(widget.integrationIdentifier,
            widget.type, widget.triggerOrActionIdentifier);

    return BaseScaffold(
      title:
          '${widget.type == AutomationChoiceEnum.trigger ? 'Trigger' : 'Action'} parameters',
      getBack: true,
      body:
          _Body(widget: widget, properties: properties, isEdit: widget.isEdit),
    );
  }
}

class _Body extends StatelessWidget {
  final AutomationParametersView widget;
  final Map<String, AutomationSchemaTriggerActionProperty> properties;
  final bool isEdit;

  const _Body({
    required this.widget,
    required this.properties,
    required this.isEdit,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(4.0),
      child: Column(
        children: [
          Expanded(
            child: _List(
              type: widget.type,
              integrationIdentifier: widget.integrationIdentifier,
              triggerOrActionIdentifier: widget.triggerOrActionIdentifier,
              properties: properties,
              indexOfTheTriggerOrAction: widget.indexOfTheTriggerOrAction,
            ),
          ),
          _OKButton(
            isEdit: isEdit,
            type: widget.type,
            indexOfTheTriggerOrAction: widget.indexOfTheTriggerOrAction,
          ),
        ],
      ),
    );
  }
}

class _OKButton extends StatelessWidget {
  final bool isEdit;
  final AutomationChoiceEnum type;
  final int indexOfTheTriggerOrAction;

  const _OKButton({
    required this.isEdit,
    required this.type,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    final Automation automation = context.select(
      (AutomationBloc bloc) => bloc.state.dirtyAutomation,
    );
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);

    final isValid = type == AutomationChoiceEnum.trigger
        ? _validateTrigger(automation, automationMediator)
        : _validateAction(
            automation, automationMediator, indexOfTheTriggerOrAction);

    return Row(
      children: [
        Expanded(
          child: TriggoButton(
            text: "OK",
            onPressed: isValid
                ? () {
                    context
                        .read<AutomationBloc>()
                        .add(AutomationLoadDirtyToClean());
                    if (isEdit) {
                      Navigator.of(context).pop();
                    } else {
                      Navigator.of(context)
                        ..pop()
                        ..pop()
                        ..pop()
                        ..pop();
                    }
                  }
                : null,
            padding:
                const EdgeInsets.symmetric(horizontal: 20.0, vertical: 12.0),
            style: TextStyle(
              color: Theme.of(context).colorScheme.onPrimary,
              fontFamily: containerTitle.fontFamily,
              fontSize: 20,
              fontWeight: containerTitle.fontWeight,
              letterSpacing: containerTitle.letterSpacing,
            ),
          ),
        ),
      ],
    );
  }

  bool _validateTrigger(
      Automation automation, AutomationMediator automationMediator) {
    final schema = automationMediator.automationSchemas;
    final trigger = automation.trigger;

    if (trigger.identifier.isEmpty) {
      log("Trigger identifier is empty");
      return false;
    }

    if (trigger.dependencies.isEmpty) {
      log("Trigger depencencies is empty");
      return false;
    }

    for (final parameter in trigger.parameters) {
      if (parameter.value.isEmpty) {
        log("Trigger parameter value is empty");
        return false;
      }
    }

    final integrationIdentifier = trigger.identifier.split('.').first;
    final triggerOrActionIdentifier = trigger.identifier.split('.').last;

    final integrationSchema = schema!.schemas[integrationIdentifier];
    final triggerSchema =
        integrationSchema?.triggers[triggerOrActionIdentifier];
    final hasDifferentParameterLength =
        triggerSchema?.parameters.length != trigger.parameters.length;

    if (integrationSchema != null &&
        triggerSchema != null &&
        hasDifferentParameterLength) {
      log("Trigger parameters length is different: ${trigger.parameters.length} - ${triggerSchema.parameters.length}");
      return false;
    }

    return true;
  }

  bool _validateAction(Automation automation,
      AutomationMediator automationMediator, int indexOfTheTriggerOrAction) {
    final schema = automationMediator.automationSchemas;

    if (indexOfTheTriggerOrAction < 0 ||
        indexOfTheTriggerOrAction >= automation.actions.length) {
      log("Index of the trigger or action is out of bounds");
      return false;
    }

    log("Index of the trigger or action: $indexOfTheTriggerOrAction");
    final action = automation.actions[indexOfTheTriggerOrAction];

    if (action.identifier.isEmpty) {
      log("Action identifier is empty");
      return false;
    }

    if (action.dependencies.isEmpty) {
      log("Action dependencies is empty");
      return false;
    }

    for (final parameter in action.parameters) {
      if (parameter.value.isEmpty) {
        log("Action parameter value is empty");
        return false;
      }
    }

    final integrationIdentifier = action.identifier.split('.').first;
    final triggerOrActionIdentifier = action.identifier.split('.').last;

    final integrationSchema = schema!.schemas[integrationIdentifier];
    final actionSchema = integrationSchema?.actions[triggerOrActionIdentifier];
    final hasDifferentParameterLength =
        actionSchema?.parameters.length != action.parameters.length;

    if (integrationSchema != null &&
        actionSchema != null &&
        hasDifferentParameterLength) {
      log("Action parameters length is different: ${action.parameters.length} - ${actionSchema.parameters.length}");
      return false;
    }

    return true;
  }
}

class _List extends StatelessWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final String triggerOrActionIdentifier;
  final Map<String, AutomationSchemaTriggerActionProperty> properties;
  final int indexOfTheTriggerOrAction;

  const _List({
    required this.type,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
    required this.properties,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    if (properties.isEmpty) {
      return Center(
        child: Text('No need for parameters,\n just click the OK button',
            textAlign: TextAlign.center,
            style: Theme.of(context).textTheme.labelLarge),
      );
    }
    return ListView.separated(
      itemCount: properties.length,
      itemBuilder: (context, index) {
        final parameterIdentifier = properties.keys.elementAt(index);
        final property = properties[parameterIdentifier]!;
        return BlocBuilder<AutomationBloc, AutomationState>(
          builder: (context, state) {
            final title = property.name;
            final previewData = getPreviewData(
                state.dirtyAutomation,
                type,
                integrationIdentifier,
                indexOfTheTriggerOrAction,
                triggerOrActionIdentifier,
                parameterIdentifier,
                state.previews,
                true);
            final selectedValue = getPreviewData(
                state.dirtyAutomation,
                type,
                integrationIdentifier,
                indexOfTheTriggerOrAction,
                triggerOrActionIdentifier,
                parameterIdentifier,
                state.previews,
                false);
            final AutomationParameterNeedOptions options = haveOptions(
                state.dirtyAutomation,
                type,
                integrationIdentifier,
                indexOfTheTriggerOrAction,
                triggerOrActionIdentifier,
                parameterIdentifier);
            return AutomationLabelParameterWidget(
                title: title,
                previewData: previewData,
                disabled: options == AutomationParameterNeedOptions.blocked,
                input: options != AutomationParameterNeedOptions.no
                    ? AutomationInputView(
                        type: (options == AutomationParameterNeedOptions.yes
                            ? (parameterIdentifier == "Icon"
                                ? AutomationInputEnum.emoji
                                : AutomationInputEnum.radio)
                            : getType(property.type)),
                        label: title,
                        routeToGoWhenSave: RoutesNames.popOneTime,
                        value: selectedValue ?? previewData,
                        humanReadableValue: previewData,
                        getOptions: () async {
                          final integrationMediator =
                              RepositoryProvider.of<IntegrationMediator>(
                                  context);
                          late List<AutomationRadioModel> options;
                          try {
                            options = await getOptionsFromMediator(
                                state.dirtyAutomation,
                                type,
                                integrationIdentifier,
                                indexOfTheTriggerOrAction,
                                triggerOrActionIdentifier,
                                parameterIdentifier,
                                integrationMediator);
                          } catch (e) {
                            if (context.mounted) {
                              ScaffoldMessenger.of(context)
                                ..removeCurrentSnackBar()
                                ..showSnackBar(SnackBar(
                                    content: Text('Error getting options')));
                            }
                          }
                          return options;
                        },
                        onSave: (value, humanReadableValue) {
                          if (type == AutomationChoiceEnum.trigger) {
                            context.read<AutomationBloc>().add(
                                AutomationPreviewUpdated(
                                    key:
                                        "trigger.$indexOfTheTriggerOrAction.$integrationIdentifier.$triggerOrActionIdentifier.$parameterIdentifier",
                                    value: humanReadableValue));

                            context
                                .read<AutomationBloc>()
                                .add(AutomationTriggerParameterChanged(
                                  parameterIdentifier: parameterIdentifier,
                                  parameterValue: value,
                                ));
                          } else {
                            context.read<AutomationBloc>().add(
                                AutomationPreviewUpdated(
                                    key:
                                        "action.$indexOfTheTriggerOrAction.$integrationIdentifier.$triggerOrActionIdentifier.$parameterIdentifier",
                                    value: humanReadableValue));

                            print("Save action parameter with value: $value");
                            context
                                .read<AutomationBloc>()
                                .add(AutomationActionParameterChanged(
                                  index: indexOfTheTriggerOrAction,
                                  parameterIdentifier: parameterIdentifier,
                                  parameterValue: value,
                                  parameterType: "raw",
                                ));
                          }
                        },
                      )
                    : AutomationParameterChoice(
                        title: title,
                        type: type,
                        automation: state.cleanedAutomation,
                        value: selectedValue,
                        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
                        property: property,
                        onSave: (value, valueType, humanReadableValue,
                            indexVariable) {
                          if (type == AutomationChoiceEnum.trigger) {
                            context.read<AutomationBloc>().add(
                                AutomationPreviewUpdated(
                                    key:
                                        "trigger.$indexOfTheTriggerOrAction.$integrationIdentifier.$triggerOrActionIdentifier.$parameterIdentifier",
                                    value: humanReadableValue));

                            context
                                .read<AutomationBloc>()
                                .add(AutomationTriggerParameterChanged(
                                  parameterIdentifier: parameterIdentifier,
                                  parameterValue: value,
                                ));
                          } else {
                            context.read<AutomationBloc>().add(
                                AutomationPreviewUpdated(
                                    key:
                                        "action.$indexOfTheTriggerOrAction.$integrationIdentifier.$triggerOrActionIdentifier.$parameterIdentifier",
                                    value: valueType == 'var'
                                        ? 'From a previous trigger/action'
                                        : 'Manual input'));

                            context
                                .read<AutomationBloc>()
                                .add(AutomationActionParameterChanged(
                                  index: indexOfTheTriggerOrAction,
                                  parameterIdentifier: parameterIdentifier,
                                  parameterValue: (valueType == 'var'
                                          ? indexVariable
                                          : "") +
                                      value,
                                  parameterType: valueType,
                                ));
                          }
                        },
                      ));
          },
        );
      },
      separatorBuilder: (context, index) {
        return const SizedBox(height: 8.0);
      },
    );
  }
}

class AutomationParameterChoice extends StatelessWidget {
  final String title;
  final void Function(String, String, String, String) onSave;
  final AutomationChoiceEnum type;
  final Automation automation;
  final List<AutomationRadioModel>? options;
  final String? value;
  final String? selectedValue;
  final int indexOfTheTriggerOrAction;
  final AutomationSchemaTriggerActionProperty property;

  const AutomationParameterChoice({
    super.key,
    required this.title,
    required this.onSave,
    required this.type,
    required this.automation,
    this.options,
    this.value,
    this.selectedValue,
    required this.indexOfTheTriggerOrAction,
    required this.property,
  });

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
        title: title,
        getBack: true,
        body: ListView(
          children: [
            AutomationLabelParameterWidget(
              title: "From a previous trigger/action",
              previewData:
                  "Select a value that resulted from a previous trigger/action",
              input: AutomationParameterFromActions(
                type: type,
                automation: automation,
                label: title,
                onSave: onSave,
                options: options,
                value: value,
                indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
                property: property,
              ),
            ),
            const SizedBox(height: 8.0),
            AutomationLabelParameterWidget(
              title: "Manual input",
              previewData: "Enter manually a value",
              input: AutomationInputView(
                type: getType(property.type),
                label: title,
                routeToGoWhenSave: RoutesNames.popTwoTimes,
                onSave: (value, humanReadableValue) {
                  onSave(value, 'raw', humanReadableValue, "any");
                },
                value: selectedValue,
              ),
            ),
          ],
        ));
  }
}

class AutomationParameterFromActions extends StatelessWidget {
  final AutomationChoiceEnum type;
  final Automation automation;
  final String label;
  final String? placeholder;
  final List<AutomationRadioModel>? options;
  final void Function(String, String, String, String) onSave;
  final String? value;
  final int indexOfTheTriggerOrAction;
  final AutomationSchemaTriggerActionProperty property;

  const AutomationParameterFromActions({
    super.key,
    required this.automation,
    required this.type,
    required this.label,
    this.placeholder,
    this.options,
    required this.onSave,
    this.value,
    required this.indexOfTheTriggerOrAction,
    required this.property,
  });

  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    final schema = automationMediator.automationSchemas;

    if (schema == null || schema.schemas.isEmpty) {
      return const SizedBox();
    }

    final List<AutomationTrigger> triggers = [automation.trigger];
    final List<AutomationAction> actions = automation.actions;

    return BaseScaffold(
      title: 'Edit $label',
      getBack: true,
      body: Padding(
        padding: const EdgeInsets.all(0.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              ListView.separated(
                shrinkWrap: true,
                physics: const NeverScrollableScrollPhysics(),
                itemCount: triggers.length,
                itemBuilder: (context, index) {
                  final trigger = triggers[index];
                  final integrationIdentifier =
                      trigger.identifier.split('.').first;
                  final triggerIdentifier = trigger.identifier.split('.').last;

                  final integrationName =
                      schema.schemas[integrationIdentifier]?.name;
                  final triggerName = schema.schemas[integrationIdentifier]
                      ?.triggers[triggerIdentifier]?.name;
                  final facts = schema.schemas[integrationIdentifier]
                      ?.triggers[triggerIdentifier]?.facts;

                  if (facts == null ||
                      facts.isEmpty ||
                      integrationName == null ||
                      triggerName == null) {
                    return const SizedBox();
                  }

                  final options = getOptionsFromFacts(facts, property);

                  return AutomationLabelParameterWidget(
                    title: "Trigger",
                    previewData: integrationName,
                    input: AutomationInputView(
                      type: AutomationInputEnum.radio,
                      label: triggerName,
                      options: options,
                      routeToGoWhenSave: RoutesNames.popThreeTimes,
                      value: value?.split('.').last,
                      onSave: (value, humanReadableValue) {
                        onSave(value, 'var', humanReadableValue, "T.");
                      },
                    ),
                  );
                },
                separatorBuilder: (context, index) {
                  return const SizedBox(height: 8.0);
                },
              ),
              if (actions.isNotEmpty) const SizedBox(height: 8.0),
              ListView.separated(
                shrinkWrap: true,
                physics: const NeverScrollableScrollPhysics(),
                itemCount: actions.length,
                itemBuilder: (context, index) {
                  if (indexOfTheTriggerOrAction >= index) {
                    return const SizedBox();
                  }

                  final action = actions[index];
                  final integrationIdentifier =
                      action.identifier.split('.').first;
                  final actionsIdentifier = action.identifier.split('.').last;

                  final integrationName =
                      schema.schemas[integrationIdentifier]?.name;
                  final actionParameterName = schema
                      .schemas[integrationIdentifier]
                      ?.actions[actionsIdentifier]
                      ?.name;

                  final actionsName = schema.schemas[integrationIdentifier]
                      ?.actions[actionsIdentifier]?.name;
                  final facts = schema.schemas[integrationIdentifier]
                      ?.actions[actionsIdentifier]?.facts;

                  if (facts == null ||
                      facts.isEmpty ||
                      integrationName == null ||
                      actionsName == null) {
                    return const SizedBox();
                  }

                  final options = getOptionsFromFacts(facts, property);

                  return AutomationLabelParameterWidget(
                    title: "Action $index - $actionParameterName",
                    previewData: integrationName,
                    input: AutomationInputView(
                      type: AutomationInputEnum.radio,
                      label: actionsName,
                      options: options,
                      routeToGoWhenSave: RoutesNames.popThreeTimes,
                      value: value?.split('.').last,
                      onSave: (value, humanReadableValue) {
                        onSave(value, 'var', humanReadableValue, "$index.");
                      },
                    ),
                  );
                },
                separatorBuilder: (context, index) {
                  return const SizedBox(height: 8.0);
                },
              ),
            ],
          ),
        ),
      ),
    );
  }
}

String? getPreviewData(
    Automation automation,
    AutomationChoiceEnum type,
    String integrationIdentifier,
    int indexOfTheTriggerOrAction,
    String triggerOrActionIdentifier,
    String parameterIdentifier,
    Map<String, String> previews,
    bool humanReadable) {
  String? value;
  bool isNotHumanReadable = true;
  switch (type) {
    case AutomationChoiceEnum.trigger:
      if (automation.trigger.identifier !=
          "$integrationIdentifier.$triggerOrActionIdentifier") {
        break;
      }
      for (final parameter in automation.trigger.parameters) {
        if (parameter.identifier == parameterIdentifier) {
          value = parameter.value;
          break;
        }
      }
      break;
    case AutomationChoiceEnum.action:
      for (final action in automation.actions) {
        if (action.identifier !=
            "$integrationIdentifier.$triggerOrActionIdentifier") {
          break;
        }
        for (final parameter in action.parameters) {
          if (parameter.identifier == parameterIdentifier) {
            value = parameter.value;
            isNotHumanReadable = parameter.type != 'raw';
            break;
          }
        }
      }
      break;
  }
  if (humanReadable || !isNotHumanReadable) {
    return replaceByHumanReadable(
        type,
        integrationIdentifier,
        indexOfTheTriggerOrAction,
        triggerOrActionIdentifier,
        parameterIdentifier,
        previews);
  }
  return value;
}

String? replaceByHumanReadable(
    AutomationChoiceEnum type,
    String integrationIdentifier,
    int indexOfTheTriggerOrAction,
    String triggerOrActionIdentifier,
    String parameterIdentifier,
    Map<String, String> previews) {
  String key = '';
  if (type == AutomationChoiceEnum.trigger) {
    key = 'trigger';
  } else {
    key = 'action';
  }
  key += '.$indexOfTheTriggerOrAction';
  key += '.$integrationIdentifier';
  key += '.$triggerOrActionIdentifier';
  key += '.$parameterIdentifier';
  return previews[key];
}

Future<List<AutomationRadioModel>> getOptionsFromMediator(
    Automation automation,
    AutomationChoiceEnum type,
    String integrationName,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier,
    IntegrationMediator integrationMediator) async {
  List<AutomationRadioModel> options = [];

  switch (type) {
    case AutomationChoiceEnum.trigger:
      if (integrationName == IntegrationNames.discord) {
        if (propertyIdentifier == 'MessageReceivedInChannel') {
          if (parameterIdentifier == 'GuildId') {
            final integrationId = automation.trigger.dependencies[0];

            return await integrationMediator.discord
                .getGuildsRadio(integrationId);
          }
          if (parameterIdentifier == 'ChannelId') {
            if (automation.trigger.parameters.isEmpty) {
              return [];
            }
            final integrationId = automation.trigger.dependencies[0];
            var guildId = '';

            for (final parameter in automation.trigger.parameters) {
              if (parameter.identifier == 'GuildId') {
                guildId = parameter.value;
                break;
              }
            }

            if (guildId.isEmpty) {
              return [];
            }

            return await integrationMediator.discord
                .getChannelsRadio(integrationId, guildId);
          }
        }
      }
      if (integrationName == IntegrationNames.notion) {
        if (propertyIdentifier == 'DatabaseRowCreated' ||
            propertyIdentifier == 'DatabaseRowDeleted') {
          if (parameterIdentifier == 'DatabaseId') {
            final integrationId = automation.trigger.dependencies[0];

            return await integrationMediator.notion
                .getDatabasesRadio(integrationId);
          }
        }
      }
      break;
    case AutomationChoiceEnum.action:
      if (integrationName == IntegrationNames.discord) {
        if (propertyIdentifier == 'SendMessageToChannel') {
          if (parameterIdentifier == 'GuildId') {
            final integrationId =
                automation.actions[indexOfTheTriggerOrAction].dependencies[0];

            return await integrationMediator.discord
                .getGuildsRadio(integrationId);
          }
          if (parameterIdentifier == 'ChannelId') {
            if (automation
                .actions[indexOfTheTriggerOrAction].parameters.isEmpty) {
              return [];
            }
            final integrationId =
                automation.actions[indexOfTheTriggerOrAction].dependencies[0];
            var guildId = '';

            for (final parameter
                in automation.actions[indexOfTheTriggerOrAction].parameters) {
              if (parameter.identifier == 'GuildId') {
                guildId = parameter.value;
                break;
              }
            }

            if (guildId.isEmpty) {
              return [];
            }

            return await integrationMediator.discord
                .getChannelsRadio(integrationId, guildId);
          }
        }
      }
      if (integrationName == IntegrationNames.notion) {
        if (propertyIdentifier == 'CreateDatabase' ||
            propertyIdentifier == 'CreatePage') {
          if (parameterIdentifier == 'ParentId') {
            final integrationId =
                automation.actions[indexOfTheTriggerOrAction].dependencies[0];
            return await integrationMediator.notion
                .getPagesRadio(integrationId);
          }
        }

        if (propertyIdentifier == 'CreateDatabaseRow') {
          if (parameterIdentifier == 'DatabaseId') {
            final integrationId =
                automation.actions[indexOfTheTriggerOrAction].dependencies[0];
            return await integrationMediator.notion
                .getDatabasesRadio(integrationId);
          }
        }

        if (propertyIdentifier == 'ArchiveDatabase') {
          if (parameterIdentifier == 'DatabaseId') {
            final integrationId =
                automation.actions[indexOfTheTriggerOrAction].dependencies[0];
            return await integrationMediator.notion
                .getDatabasesRadio(integrationId);
          }
        }

        if (propertyIdentifier == 'ArchivePage') {
          if (parameterIdentifier == 'PageId') {
            final integrationId =
                automation.actions[indexOfTheTriggerOrAction].dependencies[0];
            return await integrationMediator.notion
                .getPagesRadio(integrationId);
          }
        }
      }
      break;
  }
  return options;
}

AutomationParameterNeedOptions haveOptions(
    Automation automation,
    AutomationChoiceEnum type,
    String integrationName,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier) {
  switch (type) {
    case AutomationChoiceEnum.trigger:
      if (integrationName == IntegrationNames.discord) {
        if (propertyIdentifier == 'MessageReceivedInChannel') {
          if (parameterIdentifier == 'GuildId') {
            return AutomationParameterNeedOptions.yes;
          }
          if (parameterIdentifier == 'ChannelId') {
            if (automation.trigger.parameters.isEmpty) {
              return AutomationParameterNeedOptions.blocked;
            }

            return AutomationParameterNeedOptions.yes;
          }
        }
      }
      if (integrationName == IntegrationNames.notion) {
        if (propertyIdentifier == 'DatabaseRowCreated' ||
            propertyIdentifier == 'DatabaseRowDeleted') {
          if (parameterIdentifier == 'DatabaseId') {
            return AutomationParameterNeedOptions.yes;
          }
        }
      }
      break;
    case AutomationChoiceEnum.action:
      if (integrationName == IntegrationNames.discord) {
        if (propertyIdentifier == 'SendMessageToChannel') {
          if (parameterIdentifier == 'GuildId') {
            return AutomationParameterNeedOptions.yes;
          }
          if (parameterIdentifier == 'ChannelId') {
            for (final parameter
                in automation.actions[indexOfTheTriggerOrAction].parameters) {
              if (parameter.identifier == 'GuildId') {
                if (parameter.value.isNotEmpty) {
                  return AutomationParameterNeedOptions.yes;
                }
              }
            }

            return AutomationParameterNeedOptions.blocked;
          }
        }
      }
      if (integrationName == IntegrationNames.notion) {
        if (parameterIdentifier == "Icon") {
          return AutomationParameterNeedOptions.yes;
        }

        if (propertyIdentifier == 'CreateDatabase' ||
            propertyIdentifier == 'CreatePage') {
          if (parameterIdentifier == 'ParentId') {
            return AutomationParameterNeedOptions.yes;
          }
        }

        if (propertyIdentifier == 'CreateDatabaseRow') {
          if (parameterIdentifier == 'DatabaseId') {
            return AutomationParameterNeedOptions.yes;
          }
        }

        if (propertyIdentifier == 'ArchiveDatabase') {
          if (parameterIdentifier == 'DatabaseId') {
            return AutomationParameterNeedOptions.yes;
          }
        }

        if (propertyIdentifier == 'ArchivePage') {
          if (parameterIdentifier == 'PageId') {
            return AutomationParameterNeedOptions.yes;
          }
        }
      }
      break;
  }
  return AutomationParameterNeedOptions.no;
}

List<AutomationRadioModel> getOptionsFromFacts(
    Map<String, AutomationSchemaTriggerActionProperty> facts,
    AutomationSchemaTriggerActionProperty property) {
  List<AutomationRadioModel> options = [];
  for (final fact in facts.entries) {
    if (fact.value.type != property.type) {
      continue;
    }
    options.add(AutomationRadioModel(
      title: fact.value.name,
      description: fact.value.description,
      value: fact.key,
    ));
  }
  return options;
}

AutomationInputEnum getType(String type) {
  switch (type) {
    case 'String':
      return AutomationInputEnum.text;
    case 'Float':
      return AutomationInputEnum.number;
    case 'Integer':
      return AutomationInputEnum.number;
    case 'Boolean':
      return AutomationInputEnum.boolean;
    case 'Datetime':
      return AutomationInputEnum.date;
    default:
      return AutomationInputEnum.text;
  }
}
