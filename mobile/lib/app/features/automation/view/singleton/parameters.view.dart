import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/utils/human_readable.dart';
import 'package:triggo/app/features/automation/utils/input_type.dart';
import 'package:triggo/app/features/automation/utils/parameter_get_options.dart';
import 'package:triggo/app/features/automation/utils/parameter_have_options.dart';
import 'package:triggo/app/features/automation/utils/validate.dart';
import 'package:triggo/app/features/automation/view/singleton/input.view.dart';
import 'package:triggo/app/features/automation/view/singleton/settings.view.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/theme/fonts/fonts.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationParametersView extends StatefulWidget {
  final AutomationTriggerOrActionType type;
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
          '${widget.type == AutomationTriggerOrActionType.trigger ? 'Trigger' : 'Action'} parameters',
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
  final AutomationTriggerOrActionType type;
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

    final isValid = type == AutomationTriggerOrActionType.trigger
        ? validateTrigger(automation, automationMediator)
        : validateAction(
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
}

class _List extends StatelessWidget {
  final AutomationTriggerOrActionType type;
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
            final parameterData = getHumanReadableValue(
                state.dirtyAutomation,
                type,
                integrationIdentifier,
                indexOfTheTriggerOrAction,
                triggerOrActionIdentifier,
                parameterIdentifier,
                state.previews);
            final selectedValue = parameterData?.value;
            final previewData = replaceByHumanReadable(
                    type,
                    integrationIdentifier,
                    indexOfTheTriggerOrAction,
                    triggerOrActionIdentifier,
                    parameterIdentifier,
                    state.previews) ??
                selectedValue;
            final parameterType = getParameterType(
                state.dirtyAutomation,
                type,
                integrationIdentifier,
                indexOfTheTriggerOrAction,
                triggerOrActionIdentifier,
                parameterIdentifier);
            return AutomationLabelParameterWidget(
                title: title,
                previewData: previewData,
                disabled: parameterType ==
                    AutomationParameterType.restrictedRadioBlocked,
                input: parameterType != AutomationParameterType.choice
                    ? AutomationInputView(
                        type: getInputType(parameterType),
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
                            options = await getParameterOptions(
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
                          if (type == AutomationTriggerOrActionType.trigger) {
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
                        parameterType: parameterType,
                        previousValueType: parameterData?.type,
                        onSave: (value, valueType, humanReadableValue,
                            indexVariable) {
                          if (type == AutomationTriggerOrActionType.trigger) {
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
                                        : value));

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
  final AutomationTriggerOrActionType type;
  final Automation automation;
  final String? value;
  final int indexOfTheTriggerOrAction;
  final AutomationSchemaTriggerActionProperty property;
  final AutomationParameterType parameterType;
  final String? previousValueType;

  const AutomationParameterChoice({
    super.key,
    required this.title,
    required this.onSave,
    required this.type,
    required this.automation,
    required this.value,
    required this.indexOfTheTriggerOrAction,
    required this.property,
    required this.parameterType,
    required this.previousValueType,
  });

  @override
  Widget build(BuildContext context) {
    print("Previous value type: $previousValueType");
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
                type: getInputType(parameterType),
                label: title,
                routeToGoWhenSave: RoutesNames.popTwoTimes,
                onSave: (value, humanReadableValue) {
                  onSave(value, 'raw', humanReadableValue, "any");
                },
                value: previousValueType?.toLowerCase() == 'raw' ? value : null,
              ),
            ),
          ],
        ));
  }
}

class AutomationParameterFromActions extends StatelessWidget {
  final AutomationTriggerOrActionType type;
  final Automation automation;
  final String label;
  final String? placeholder;
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

                  final options = getTypeFromFacts(facts, property);

                  return AutomationLabelParameterWidget(
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
                  if (index >= indexOfTheTriggerOrAction) {
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

                  final options = getTypeFromFacts(facts, property);

                  return AutomationLabelParameterWidget(
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
