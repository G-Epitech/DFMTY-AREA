import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation_creation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/view/creation/input.view.dart';
import 'package:triggo/app/features/automation/view/creation/settings.view.dart';
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
    final List<AutomationSchemaTriggerActionProperty> properties =
        automationMediator.getParameters(
            widget.integrationName, widget.type, widget.parameterIdentifier);
    return BaseScaffold(
      title:
          'Parameters of the ${widget.type == AutomationChoiceEnum.trigger ? 'Trigger' : 'Action'}',
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
  final List<AutomationSchemaTriggerActionProperty> properties;

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
        final property = properties[index];
        return BlocBuilder<AutomationCreationBloc, AutomationCreationState>(
          builder: (context, state) {
            final title = property.name;
            final previewData = getPreviewData(
                state.automation, type, parameterIdentifier, index);
            return AutomationLabelParameterWidget(
              title: title,
              previewData: previewData,
              input: AutomationCreationInputView(
                type: AutomationInputEnum.text,
                label: 'Label',
                placeholder: 'Enter a label',
                onValueChanged: (value) {
                  print(value);
                  /*context
                  .read<AutomationCreationBloc>()
                  .add(AutomationCreationLabelChanged(label: value));*/
                },
                value: state.automation.label,
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
