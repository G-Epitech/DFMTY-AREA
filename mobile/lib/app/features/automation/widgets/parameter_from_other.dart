import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/utils/parameter_have_options.dart';
import 'package:triggo/app/features/automation/view/singleton/input.view.dart';
import 'package:triggo/app/features/automation/widgets/input_parameter.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationParameterFromOther extends StatelessWidget {
  final AutomationTriggerOrActionType type;
  final Automation automation;
  final String label;
  final String? placeholder;
  final void Function(String, String, String, String) onSave;
  final String? value;
  final int indexOfTheTriggerOrAction;
  final AutomationSchemaTriggerActionProperty property;

  const AutomationParameterFromOther({
    super.key,
    required this.automation,
    required this.type,
    required this.label,
    this.placeholder,
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
              _buildTriggersList(context, triggers),
              if (actions.isNotEmpty) const SizedBox(height: 8.0),
              _buildActionsList(context, actions),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildTriggersList(
      BuildContext context, List<AutomationTrigger> triggers) {
    return ListView.separated(
      shrinkWrap: true,
      physics: const NeverScrollableScrollPhysics(),
      itemCount: triggers.length,
      itemBuilder: (context, index) =>
          _buildTriggerItem(context, triggers[index]),
      separatorBuilder: (context, index) => const SizedBox(height: 8.0),
    );
  }

  Widget _buildTriggerItem(BuildContext context, AutomationTrigger trigger) {
    final integrationIdentifier = trigger.identifier.split('.').first;
    final triggerIdentifier = trigger.identifier.split('.').last;
    final integrationName = _getIntegrationName(context, integrationIdentifier);
    final triggerName =
        _getTriggerName(context, integrationIdentifier, triggerIdentifier);
    final facts = _getFacts(context, integrationIdentifier, triggerIdentifier);

    if (facts == null ||
        facts.isEmpty ||
        integrationName == null ||
        triggerName == null) {
      return const SizedBox();
    }

    final options = getTypeFromFacts(facts, property);

    return AutomationInputParameterWithLabel(
      title: "Trigger",
      previewData: integrationName,
      input: AutomationInputView(
        type: AutomationInputType.radio,
        label: triggerName,
        options: options,
        routeToGoWhenSave: RoutesNames.popThreeTimes,
        value: value?.split('.').last,
        onSave: (value, humanReadableValue) {
          onSave(value, 'var', humanReadableValue, "T.");
        },
      ),
    );
  }

  Widget _buildActionsList(
      BuildContext context, List<AutomationAction> actions) {
    return ListView.separated(
      shrinkWrap: true,
      physics: const NeverScrollableScrollPhysics(),
      itemCount: actions.length,
      itemBuilder: (context, index) =>
          _buildActionItem(context, actions[index], index),
      separatorBuilder: (context, index) => const SizedBox(height: 8.0),
    );
  }

  Widget _buildActionItem(
      BuildContext context, AutomationAction action, int index) {
    if (index >= indexOfTheTriggerOrAction) {
      return const SizedBox();
    }

    final integrationIdentifier = action.identifier.split('.').first;
    final actionsIdentifier = action.identifier.split('.').last;
    final integrationName = _getIntegrationName(context, integrationIdentifier);
    final actionParameterName =
        _getActionName(context, integrationIdentifier, actionsIdentifier);
    final actionsName =
        _getActionName(context, integrationIdentifier, actionsIdentifier);
    final facts = _getFacts(context, integrationIdentifier, actionsIdentifier);

    if (facts == null ||
        facts.isEmpty ||
        integrationName == null ||
        actionsName == null) {
      return const SizedBox();
    }

    final options = getTypeFromFacts(facts, property);

    return AutomationInputParameterWithLabel(
      title: "Action $index - $actionParameterName",
      previewData: integrationName,
      input: AutomationInputView(
        type: AutomationInputType.radio,
        label: actionsName,
        options: options,
        routeToGoWhenSave: RoutesNames.popThreeTimes,
        value: value?.split('.').last,
        onSave: (value, humanReadableValue) {
          onSave(value, 'var', humanReadableValue, "$index.");
        },
      ),
    );
  }

  String? _getIntegrationName(
      BuildContext context, String integrationIdentifier) {
    final schema =
        RepositoryProvider.of<AutomationMediator>(context).automationSchemas;
    return schema?.schemas[integrationIdentifier]?.name;
  }

  String? _getTriggerName(BuildContext context, String integrationIdentifier,
      String triggerIdentifier) {
    final schema =
        RepositoryProvider.of<AutomationMediator>(context).automationSchemas;
    return schema
        ?.schemas[integrationIdentifier]?.triggers[triggerIdentifier]?.name;
  }

  String? _getActionName(BuildContext context, String integrationIdentifier,
      String actionsIdentifier) {
    final schema =
        RepositoryProvider.of<AutomationMediator>(context).automationSchemas;
    return schema
        ?.schemas[integrationIdentifier]?.actions[actionsIdentifier]?.name;
  }

  Map<String, AutomationSchemaTriggerActionProperty>? _getFacts(
      BuildContext context, String integrationIdentifier, String identifier) {
    final schema =
        RepositoryProvider.of<AutomationMediator>(context).automationSchemas;
    return schema
            ?.schemas[integrationIdentifier]?.triggers[identifier]?.facts ??
        schema?.schemas[integrationIdentifier]?.actions[identifier]?.facts;
  }
}
