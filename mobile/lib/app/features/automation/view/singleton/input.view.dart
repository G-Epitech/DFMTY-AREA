import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/widgets/input/emoji.dart';
import 'package:triggo/app/features/automation/widgets/input/number.dart';
import 'package:triggo/app/features/automation/widgets/input/radio.dart';
import 'package:triggo/app/features/automation/widgets/input/text.dart';
import 'package:triggo/app/features/automation/widgets/input/text_area.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationInputView extends StatefulWidget {
  final AutomationInputType type;
  final String label;
  final String? placeholder;
  final List<AutomationRadioModel>? options;
  final List<AutomationCheckboxModel>? checkboxes;
  final void Function(String, String)? onSave;
  final void Function(List<String>, List<String>)? onCheckboxesSave;
  final String? value;
  final List<String>? checkboxesValues;
  final String? humanReadableValue;
  final List<String>? humanReadableCheckboxesValues;
  final String routeToGoWhenSave;
  final Future<List<AutomationRadioModel>> Function()? getOptions;
  final Future<List<AutomationCheckboxModel>> Function()? getCheckboxes;

  const AutomationInputView({
    super.key,
    required this.type,
    required this.label,
    this.placeholder,
    this.options,
    this.checkboxes,
    this.onSave,
    this.onCheckboxesSave,
    this.value,
    this.checkboxesValues,
    this.humanReadableValue,
    this.humanReadableCheckboxesValues,
    required this.routeToGoWhenSave,
    this.getOptions,
    this.getCheckboxes,
  });

  @override
  State<AutomationInputView> createState() => _AutomationInputViewState();
}

class _AutomationInputViewState extends State<AutomationInputView> {
  late String localValue;
  late List<String> checkboxesValues;
  late String humanReadableValue;
  late List<String> humanReadableCheckboxesValues;

  @override
  void initState() {
    super.initState();
    localValue = widget.value ?? "";
    checkboxesValues = widget.checkboxesValues ?? [];
    humanReadableValue = widget.humanReadableValue ?? "";
    humanReadableCheckboxesValues = widget.humanReadableCheckboxesValues ?? [];
  }

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: 'Edit ${widget.label}',
      getBack: true,
      body: Padding(
        padding: const EdgeInsets.all(4.0),
        child: Column(
          children: [
            Expanded(child: _buildInput()),
            _OKButton(
              onSave: widget.onSave,
              onCheckboxesSave: widget.onCheckboxesSave,
              value: localValue,
              checkboxesValues: checkboxesValues,
              humanReadableValue: humanReadableValue,
              humanReadableCheckboxesValues: humanReadableCheckboxesValues,
              routeToGoWhenSave: widget.routeToGoWhenSave,
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildInput() {
    switch (widget.type) {
      case AutomationInputType.text:
        return TextInput(
          label: widget.label,
          placeholder: widget.placeholder,
          defaultValue: localValue,
          onValueChanged: (value) {
            setState(() {
              localValue = value;
              humanReadableValue = value;
            });
          },
        );
      case AutomationInputType.textArea:
        return TextAreaInput(
          label: widget.label,
          placeholder: widget.placeholder,
          defaultValue: localValue,
          onChanged: (value) {
            setState(() {
              localValue = value;
              humanReadableValue = value;
            });
          },
        );
      case AutomationInputType.radio:
        return RadioInput(
          label: widget.label,
          options: widget.options,
          defaultValue: localValue,
          onChanged: (value, humanValue) {
            setState(() {
              localValue = value;
              humanReadableValue = humanValue;
            });
          },
          getOptions: widget.getOptions,
        );
      case AutomationInputType.emoji:
        return EmojiInput(
          label: widget.label,
          placeholder: widget.placeholder ?? 'Enter an emoji',
          defaultValue: localValue,
          onValueChanged: (value) {
            setState(() {
              localValue = value;
              humanReadableValue = value;
            });
          },
        );
      case AutomationInputType.number:
        return NumberInput(
          label: widget.label,
          placeholder: widget.placeholder,
          defaultValue: localValue,
          onValueChanged: (value) {
            setState(() {
              localValue = value;
              humanReadableValue = value;
            });
          },
        );
      default:
        return Container();
    }
  }
}

class _OKButton extends StatelessWidget {
  final void Function(String, String)? onSave;
  final void Function(List<String>, List<String>)? onCheckboxesSave;
  final String value;
  final List<String> checkboxesValues;
  final String humanReadableValue;
  final List<String> humanReadableCheckboxesValues;
  final String routeToGoWhenSave;

  const _OKButton({
    required this.onSave,
    required this.onCheckboxesSave,
    required this.value,
    required this.checkboxesValues,
    required this.humanReadableValue,
    required this.humanReadableCheckboxesValues,
    required this.routeToGoWhenSave,
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
                if (onCheckboxesSave != null) {
                  onCheckboxesSave!(
                      checkboxesValues, humanReadableCheckboxesValues);
                } else {
                  onSave!(value, humanReadableValue);
                }
                if (routeToGoWhenSave == RoutesNames.popOneTime) {
                  Navigator.of(context).pop();
                } else if (routeToGoWhenSave == RoutesNames.popTwoTimes) {
                  Navigator.of(context)
                    ..pop()
                    ..pop();
                } else if (routeToGoWhenSave == RoutesNames.popThreeTimes) {
                  Navigator.of(context)
                    ..pop()
                    ..pop()
                    ..pop();
                } else {
                  Navigator.of(context).pushNamedAndRemoveUntil(
                    routeToGoWhenSave,
                    (route) => route.settings.name == routeToGoWhenSave,
                  );
                }
              },
            ),
          ),
        ],
      ),
    );
  }
}
