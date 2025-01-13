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

  const AutomationCreationParametersView({
    super.key,
    required this.type,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
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
      ),
    );
  }
}

class _List extends StatelessWidget {
  final AutomationChoiceEnum type;
  final String integrationIdentifier;
  final String triggerOrActionIdentifier;
  final Map<String, AutomationSchemaTriggerActionProperty> properties;

  const _List({
    required this.type,
    required this.integrationIdentifier,
    required this.triggerOrActionIdentifier,
    required this.properties,
  });

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: properties.length,
      itemBuilder: (context, index) {
        final key = properties.keys.elementAt(index);
        final property = properties[key]!;
        return BlocBuilder<AutomationCreationBloc, AutomationCreationState>(
          builder: (context, state) {
            final title = property.name;
            final previewData = getPreviewData(
                state.automation,
                type,
                integrationIdentifier,
                index,
                triggerOrActionIdentifier,
                state.previews);
            print("Preview data: $previewData");
            final String selectedValue = getSelectedValue(
                    state.automation, type, triggerOrActionIdentifier, index) ??
                "";
            print("Selected value: $selectedValue");
            final List<AutomationRadioModel>? options = getOptions(
                state.automation,
                type,
                integrationIdentifier,
                triggerOrActionIdentifier,
                key);
            print("Options: $options");
            return AutomationLabelParameterWidget(
              title: title,
              previewData: previewData,
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
                                "trigger.$index.$integrationIdentifier.$triggerOrActionIdentifier.$key",
                            value: humanReadableValue));
                    context
                        .read<AutomationCreationBloc>()
                        .add(AutomationCreationTriggerParameterChanged(
                          parameterIdentifier: key,
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
    );
  }
}

String getPreviewData(
    Automation automation,
    AutomationChoiceEnum type,
    String integrationIdentifier,
    int index,
    String triggerOrActionIdentifier,
    Map<String, String> previews) {
  String value = '';
  String parameterIdentifier = '';
  bool isNotHumanReadable = true;
  switch (type) {
    case AutomationChoiceEnum.trigger:
      if (automation.trigger.identifier != triggerOrActionIdentifier) {
        break;
      }
      if (automation.trigger.parameters.length > index) {
        value = automation.trigger.parameters[index].value;
        parameterIdentifier = automation.trigger.parameters[index].identifier;
      }
      break;
    case AutomationChoiceEnum.action:
      for (final action in automation.actions) {
        if (action.identifier == triggerOrActionIdentifier) {
          value = action.parameters[index].value;
          parameterIdentifier = action.parameters[index].identifier;
          isNotHumanReadable = action.parameters[index].type != 'raw';
          break;
        }
      }
      break;
  }
  if (isNotHumanReadable) {
    return replaceByHumanReadable(type, integrationIdentifier, index,
        triggerOrActionIdentifier, parameterIdentifier, previews);
  }
  return value;
}

String replaceByHumanReadable(
    AutomationChoiceEnum type,
    String integrationIdentifier,
    int index,
    String triggerOrActionIdentifier,
    String parameterIdentifier,
    Map<String, String> previews) {
  String key = '';
  if (type == AutomationChoiceEnum.trigger) {
  } else {
    key = 'action';
  }
  key = 'trigger';
  key += '.$index';
  key += '.$integrationIdentifier';
  key += '.$triggerOrActionIdentifier';
  key += '.$parameterIdentifier';
  return previews[key] ?? '';
}

String? getSelectedValue(Automation automation, AutomationChoiceEnum type,
    String propertyIdentifier, int index) {
  print("Property identifier: $propertyIdentifier");
  switch (type) {
    case AutomationChoiceEnum.trigger:
      print("Index: ${index}");
      print("Automation: ${automation.trigger.identifier}");
      if (automation.trigger.identifier == propertyIdentifier) {
        print("Parameters Length: ${automation.trigger.parameters.length}");
        if (automation.trigger.parameters.length > index) {
          print("Value: ${automation.trigger.parameters[index].value}");
          return automation.trigger.parameters[index].value;
        }
      }
      break;
    case AutomationChoiceEnum.action:
      for (final action in automation.actions) {
        if (action.identifier == propertyIdentifier) {
          if (action.parameters.isNotEmpty) {
            return action.parameters[index].value;
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
            options = [
              AutomationRadioModel(
                title: 'Guild 1',
                description: 'Guild 1 description',
                value: '1',
              ),
              AutomationRadioModel(
                title: 'Guild 2',
                description: 'Guild 2 description',
                value: '2',
              ),
            ];

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
