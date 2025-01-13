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
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationCreationParametersView extends StatefulWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final String triggerOrActionIdentifier;
  final int indexOfTheTriggerOrAction;

  const AutomationCreationParametersView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
    required this.indexOfTheTriggerOrAction,
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

    log("Properties Length: ${properties.length}");
    return BaseScaffold(
      title:
          '${widget.type == AutomationChoiceEnum.trigger ? 'Trigger' : 'Action'} parameters',
      getBack: true,
      body: _List(
        type: widget.type,
        integrationIdentifier: widget.integrationIdentifier,
        triggerOrActionIdentifier: widget.triggerOrActionIdentifier,
        properties: properties,
        indexOfTheTriggerOrAction: widget.indexOfTheTriggerOrAction,
      ),
    );
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
                state.automation,
                type,
                integrationIdentifier,
                indexOfTheTriggerOrAction,
                triggerOrActionIdentifier,
                parameterIdentifier,
                state.previews);
            print("Preview data: $previewData");
            final String selectedValue = getSelectedValue(
                    state.automation,
                    type,
                    triggerOrActionIdentifier,
                    indexOfTheTriggerOrAction) ??
                "";
            print("Selected value: $selectedValue");
            final List<AutomationRadioModel>? options = getOptions(
                state.automation,
                type,
                integrationIdentifier,
                triggerOrActionIdentifier,
                parameterIdentifier);
            return AutomationLabelParameterWidget(
              title: title,
              previewData: previewData,
              disabled: options == null,
              input: AutomationCreationInputView(
                type: options != null
                    ? AutomationInputEnum.radio
                    : AutomationInputEnum.text,
                label: title,
                routeToGoWhenSave:
                    RoutesNames.automationTriggerActionRestricted,
                value: selectedValue,
                options: options,
                onSave: (value) {
                  print(value);
                  if (type == AutomationChoiceEnum.trigger) {
                    print("Trigger parameter changed to $value");
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
                    print("Action parameter changed to $value");
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

String getPreviewData(
    Automation automation,
    AutomationChoiceEnum type,
    String integrationIdentifier,
    int indexOfTheTriggerOrAction,
    String triggerOrActionIdentifier,
    String parameterIdentifier,
    Map<String, String> previews) {
  String value = '';
  bool isNotHumanReadable = true;
  switch (type) {
    case AutomationChoiceEnum.trigger:
      if (automation.trigger.identifier != triggerOrActionIdentifier) {
        break;
      }
      for (final parameter in automation.trigger.parameters) {
        print("Parameter: ${parameter.identifier} - $parameterIdentifier");
        if (parameter.identifier == parameterIdentifier) {
          value = parameter.value;
          break;
        }
      }
      break;
    case AutomationChoiceEnum.action:
      for (final action in automation.actions) {
        if (action.identifier == triggerOrActionIdentifier) {
          for (final parameter in action.parameters) {
            if (parameter.identifier == parameterIdentifier) {
              value = parameter.value;
              isNotHumanReadable = parameter.type != 'raw';
              break;
            }
          }
        }
      }
      break;
  }
  if (isNotHumanReadable) {
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

String replaceByHumanReadable(
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
  print("replaceByHumanReadable key: $key");
  return previews[key] ?? '';
}

String? getSelectedValue(Automation automation, AutomationChoiceEnum type,
    String propertyIdentifier, int parameterIndex) {
  print("Property identifier: $propertyIdentifier");
  switch (type) {
    case AutomationChoiceEnum.trigger:
      print("Index: ${parameterIndex}");
      print("Automation: ${automation.trigger.identifier}");
      if (automation.trigger.identifier == propertyIdentifier) {
        for (final parameter in automation.trigger.parameters) {
          if (parameter.identifier == propertyIdentifier) {
            return parameter.value;
          }
        }
      }
      break;
    case AutomationChoiceEnum.action:
      for (final action in automation.actions) {
        if (action.identifier == propertyIdentifier) {
          if (action.parameters.isNotEmpty) {
            return action.parameters[parameterIndex].value;
          }
          break;
        }
      }
      break;
  }
  return null;
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
              return null;
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
        if (propertyIdentifier == 'SendMessage') {
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
