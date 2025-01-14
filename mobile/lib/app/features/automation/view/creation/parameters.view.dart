import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation_creation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/models/radio.model.dart';
import 'package:triggo/app/features/automation/view/creation/input.view.dart';
import 'package:triggo/app/features/automation/view/creation/settings.view.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/fonts/fonts.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationCreationParametersView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final String triggerOrActionIdentifier;
  final int indexOfTheTriggerOrAction;
  final bool isEdit;

  const AutomationCreationParametersView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
    required this.indexOfTheTriggerOrAction,
    this.isEdit = false,
  });

  @override
  State<AutomationCreationParametersView> createState() =>
      _AutomationCreationParametersViewState();
}

class _AutomationCreationParametersViewState
    extends State<AutomationCreationParametersView> {
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
  final AutomationCreationParametersView widget;
  final Map<String, AutomationSchemaTriggerActionProperty> properties;
  final bool isEdit;

  const _Body({
    required this.widget,
    required this.properties,
    required this.isEdit,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
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
        _OKButton(isEdit: isEdit),
      ],
    );
  }
}

class _OKButton extends StatelessWidget {
  final bool isEdit;

  const _OKButton({required this.isEdit});

  @override
  Widget build(BuildContext context) {
    final Automation automation = context.select(
      (AutomationCreationBloc bloc) => bloc.state.dirtyAutomation,
    );
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);

    final isValid = _validateTrigger(automation, automationMediator);

    return Row(
      children: [
        Expanded(
          child: TriggoButton(
            text: "OK",
            onPressed: isValid
                ? () {
                    context
                        .read<AutomationCreationBloc>()
                        .add(AutomationCreationValidatePendingAutomation());
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

    if (trigger.providers.isEmpty) {
      log("Trigger providers is empty");
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
    return ListView.separated(
      itemCount: properties.length,
      itemBuilder: (context, index) {
        final parameterIdentifier = properties.keys.elementAt(index);
        final property = properties[parameterIdentifier]!;
        return BlocBuilder<AutomationCreationBloc, AutomationCreationState>(
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
            final List<AutomationRadioModel>? options = getOptions(
                state.dirtyAutomation,
                type,
                integrationIdentifier,
                triggerOrActionIdentifier,
                parameterIdentifier);
            return AutomationLabelParameterWidget(
              title: title,
              previewData: previewData,
              disabled: options != null && options.isEmpty,
              input: AutomationCreationInputView(
                type: options != null
                    ? AutomationInputEnum.radio
                    : AutomationInputEnum.text,
                label: title,
                routeToGoWhenSave: RoutesNames.popOneTime,
                value: selectedValue,
                options: options,
                onSave: (value) {
                  if (type == AutomationChoiceEnum.trigger) {
                    final humanReadableValue = options != null
                        ? options
                            .firstWhere((element) => element.value == value)
                            .title
                        : value;

                    context.read<AutomationCreationBloc>().add(
                        AutomationCreationPreviewUpdated(
                            key:
                                "trigger.$indexOfTheTriggerOrAction.$integrationIdentifier.$triggerOrActionIdentifier.$parameterIdentifier",
                            value: humanReadableValue));

                    context
                        .read<AutomationCreationBloc>()
                        .add(AutomationCreationTriggerParameterChanged(
                          parameterIdentifier: parameterIdentifier,
                          parameterValue: value,
                        ));
                  } else {
                    /*context
                        .read<AutomationCreationBloc>()
                        .add(AutomationCreationActionParameterChanged(
                          parameterIdentifier: key,
                          parameterValue: value,
                        ));*/
                  }
                  /*context
                  .read<AutomationCreationBloc>()
                  .add(AutomationCreationLabelChanged(label: value));*/
                },
              ),
            );
          },
        );
      },
      separatorBuilder: (context, index) {
        return const SizedBox(height: 8.0);
      },
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
  if (humanReadable && isNotHumanReadable) {
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
  } else {
    key = 'action';
  }
  key = 'trigger';
  key += '.$indexOfTheTriggerOrAction';
  key += '.$integrationIdentifier';
  key += '.$triggerOrActionIdentifier';
  key += '.$parameterIdentifier';
  return previews[key];
}

List<AutomationRadioModel>? getOptions(
    Automation automation,
    AutomationChoiceEnum type,
    String integrationName,
    String propertyIdentifier,
    String parameterIdentifier) {
  List<AutomationRadioModel> options = [];

  switch (type) {
    case AutomationChoiceEnum.trigger:
      if (integrationName == 'discord') {
        if (propertyIdentifier == 'MessageReceivedInChannel') {
          if (parameterIdentifier == 'GuildId') {
            options = List.generate(
                30,
                (index) => AutomationRadioModel(
                      title: 'Guild ${index + 1}',
                      description: 'Guild ${index + 1} description',
                      value: '${index + 1}',
                    ));

            return options;
          }
          if (parameterIdentifier == 'ChannelId') {
            if (automation.trigger.parameters.isEmpty) {
              log("====== Trigger parameters is empty ======");
              return [];
            }
            final guildId = automation.trigger.parameters[0].value;
            options = List.generate(
                30,
                (index) => AutomationRadioModel(
                      title: 'Channel ${index + 1}',
                      description: 'Channel ${index + 1} description',
                      value: '${index + 1}',
                    ));

            return options;
          }
        }
      }
      break;
    case AutomationChoiceEnum.action:
      if (integrationName == 'discord') {
        if (propertyIdentifier == 'SendMessageToChannel') {
          if (parameterIdentifier == 'ChannelId') {
            options = [
              AutomationRadioModel(
                title: 'Channel 1',
                description: 'Channel 1 description',
                value: '1',
              ),
              AutomationRadioModel(
                title: 'Channel 2',
                description: 'Channel 2 description',
                value: '2',
              ),
            ];

            return options;
          }
        }
      }
      break;
  }
  return null;
}
