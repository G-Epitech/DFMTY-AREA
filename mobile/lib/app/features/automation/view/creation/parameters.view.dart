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
  final String integrationName;
  final String parameterIdentifier;

  const AutomationCreationParametersView({
    super.key,
    required this.type,
    required this.integrationName,
    required this.parameterIdentifier,
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
        automationMediator.getParameters(
            widget.integrationName, widget.type, widget.parameterIdentifier);

    log("Properties Length: ${properties.length}");
    return BaseScaffold(
      title:
          '${widget.type == AutomationChoiceEnum.trigger ? 'Trigger' : 'Action'} parameters',
      getBack: true,
      body: _List(
        type: widget.type,
        integrationName: widget.integrationName,
        parameterIdentifier: widget.parameterIdentifier,
        properties: properties,
      ),
    );
  }
}

class _List extends StatelessWidget {
  final AutomationChoiceEnum type;
  final String integrationName;
  final String parameterIdentifier;
  final Map<String, AutomationSchemaTriggerActionProperty> properties;

  const _List({
    required this.type,
    required this.integrationName,
    required this.parameterIdentifier,
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
            /*final previewData = getPreviewData(
                state.automation, type, parameterIdentifier, index);*/
            final previewData = '';
            print("Preview data: $previewData");
            final String selectedValue = getSelectedValue(
                    state.automation, type, parameterIdentifier, index) ??
                "";
            print("Selected value: $selectedValue");
            final List<AutomationRadioModel>? options = getOptions(
                state.automation,
                type,
                integrationName,
                parameterIdentifier,
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

String getPreviewData(Automation automation, AutomationChoiceEnum type,
    String propertyIdentifier, int index) {
  String value = '';
  String identifier = '';
  bool isHumanReadable = false;
  switch (type) {
    case AutomationChoiceEnum.trigger:
      value = automation.trigger.parameters[index].value;
      identifier = automation.trigger.identifier;
      break;
    case AutomationChoiceEnum.action:
      for (final action in automation.actions) {
        if (action.identifier == propertyIdentifier) {
          value = action.parameters[index].value;
          identifier = action.identifier;
          isHumanReadable = action.parameters[index].type == 'raw';
          break;
        }
      }
      break;
  }
  if (isHumanReadable) {
    return replaceByHumanReadable(identifier, value);
  }
  return value;
}

String replaceByHumanReadable(String identifier, String value) {
  String humanReadable = value;
  switch (identifier) {
    case 'discord.triggers.MessageReceivedInChannel.GuildId':
      humanReadable =
          "AutomationBloc.data['discord']['guilds'][${value}]"; // Something like this. OR no. Don't know yet.
      break;
  }
  return humanReadable;
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
