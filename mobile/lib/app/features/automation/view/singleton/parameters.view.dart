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
import 'package:triggo/app/features/automation/widgets/input_parameter.dart';
import 'package:triggo/app/features/automation/widgets/parameter_choice.dart';
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
      return _buildNoParametersMessage(context);
    }
    return ListView.separated(
      itemCount: properties.length,
      itemBuilder: (context, index) => _buildListItem(context, index),
      separatorBuilder: (context, index) => const SizedBox(height: 8.0),
    );
  }

  Widget _buildNoParametersMessage(BuildContext context) {
    return Center(
      child: Text(
        'No need for parameters,\n just click the OK button',
        textAlign: TextAlign.center,
        style: Theme.of(context).textTheme.labelLarge,
      ),
    );
  }

  Widget _buildListItem(BuildContext context, int index) {
    final parameterIdentifier = properties.keys.elementAt(index);
    final property = properties[parameterIdentifier]!;

    return BlocBuilder<AutomationBloc, AutomationState>(
      builder: (context, state) {
        final title = property.name;
        final parameterData = _getParameterModel(state, parameterIdentifier);
        final selectedValue = parameterData?.value;
        final previewData =
            _getPreviewData(state, parameterIdentifier) ?? selectedValue;
        final parameterType = _getParameterType(state, parameterIdentifier);

        return AutomationInputParameterWithLabel(
          title: title,
          previewData: previewData,
          disabled:
              parameterType == AutomationParameterType.restrictedRadioBlocked,
          input: _buildInputWidget(context, state, parameterType, title,
              selectedValue, previewData, parameterIdentifier),
        );
      },
    );
  }

  Widget _buildInputWidget(
    BuildContext context,
    AutomationState state,
    AutomationParameterType parameterType,
    String title,
    String? selectedValue,
    String? previewData,
    String parameterIdentifier,
  ) {
    if (parameterType != AutomationParameterType.choice) {
      return AutomationInputView(
        type: getInputType(parameterType),
        label: title,
        routeToGoWhenSave: RoutesNames.popOneTime,
        value: selectedValue ?? previewData,
        humanReadableValue: previewData,
        getOptions: _getOptions(context, state, parameterIdentifier),
        onSave: (value, humanReadableValue) =>
            _onSave(context, value, humanReadableValue, parameterIdentifier),
      );
    } else {
      return AutomationParameterChoice(
        title: title,
        type: type,
        automation: state.cleanedAutomation,
        value: selectedValue,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
        property: properties[parameterIdentifier]!,
        parameterType: parameterType,
        previousValueType: _getParameterModel(state, parameterIdentifier)?.type,
        onSave: (value, valueType, humanReadableValue, indexVariable) =>
            _onSaveChoice(context, value, valueType, humanReadableValue,
                indexVariable, parameterIdentifier),
      );
    }
  }

  Future<List<AutomationRadioModel>> Function() _getOptions(
      BuildContext context, AutomationState state, String parameterIdentifier) {
    return () async {
      final integrationMediator =
          RepositoryProvider.of<IntegrationMediator>(context);
      try {
        return await getParameterOptions(
          state.dirtyAutomation,
          type,
          integrationIdentifier,
          indexOfTheTriggerOrAction,
          triggerOrActionIdentifier,
          parameterIdentifier,
          integrationMediator,
        );
      } catch (e) {
        if (context.mounted) {
          ScaffoldMessenger.of(context)
            ..removeCurrentSnackBar()
            ..showSnackBar(
                const SnackBar(content: Text('Error getting options')));
        }
        return [];
      }
    };
  }

  void _onSave(BuildContext context, String value, String humanReadableValue,
      String parameterIdentifier) {
    final key = _getKey(parameterIdentifier);
    context
        .read<AutomationBloc>()
        .add(AutomationPreviewUpdated(key: key, value: humanReadableValue));
    if (type == AutomationTriggerOrActionType.trigger) {
      context.read<AutomationBloc>().add(AutomationTriggerParameterChanged(
          parameterIdentifier: parameterIdentifier, parameterValue: value));
    } else {
      context.read<AutomationBloc>().add(AutomationActionParameterChanged(
          index: indexOfTheTriggerOrAction,
          parameterIdentifier: parameterIdentifier,
          parameterValue: value,
          parameterType: "raw"));
    }
  }

  void _onSaveChoice(
      BuildContext context,
      String value,
      String valueType,
      String humanReadableValue,
      String indexVariable,
      String parameterIdentifier) {
    final key = _getKey(parameterIdentifier);
    final previewValue =
        valueType == 'var' ? 'From a previous trigger/action' : value;
    context
        .read<AutomationBloc>()
        .add(AutomationPreviewUpdated(key: key, value: previewValue));
    if (type == AutomationTriggerOrActionType.trigger) {
      context.read<AutomationBloc>().add(AutomationTriggerParameterChanged(
          parameterIdentifier: parameterIdentifier, parameterValue: value));
    } else {
      context.read<AutomationBloc>().add(AutomationActionParameterChanged(
          index: indexOfTheTriggerOrAction,
          parameterIdentifier: parameterIdentifier,
          parameterValue: (valueType == 'var' ? indexVariable : "") + value,
          parameterType: valueType));
    }
  }

  String _getKey(String parameterIdentifier) {
    return "${type == AutomationTriggerOrActionType.trigger ? "trigger" : "action"}.$indexOfTheTriggerOrAction.$integrationIdentifier.$triggerOrActionIdentifier.$parameterIdentifier";
  }

  AutomationParameterModel? _getParameterModel(
      AutomationState state, String parameterIdentifier) {
    return getHumanReadableValue(
      state.dirtyAutomation,
      type,
      integrationIdentifier,
      indexOfTheTriggerOrAction,
      triggerOrActionIdentifier,
      parameterIdentifier,
      state.previews,
    );
  }

  String? _getPreviewData(AutomationState state, String parameterIdentifier) {
    return replaceByHumanReadable(
      type,
      integrationIdentifier,
      indexOfTheTriggerOrAction,
      triggerOrActionIdentifier,
      parameterIdentifier,
      state.previews,
    );
  }

  AutomationParameterType _getParameterType(
      AutomationState state, String parameterIdentifier) {
    return getParameterType(
      state.dirtyAutomation,
      type,
      integrationIdentifier,
      indexOfTheTriggerOrAction,
      triggerOrActionIdentifier,
      parameterIdentifier,
    );
  }
}
