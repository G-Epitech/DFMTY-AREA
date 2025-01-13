import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/models/radio.model.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/button.triggo.dart';
import 'package:triggo/app/widgets/input.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationCreationInputView extends StatefulWidget {
  final AutomationInputEnum type;
  final String label;
  final String? placeholder;
  final List<AutomationRadioModel>? options;
  final void Function(String) onSave;
  final String? value;
  final String routeToGoWhenSave;

  const AutomationCreationInputView({
    super.key,
    required this.type,
    required this.label,
    this.placeholder,
    this.options,
    required this.onSave,
    this.value,
    required this.routeToGoWhenSave,
  });

  @override
  State<AutomationCreationInputView> createState() =>
      _AutomationCreationInputViewState();
}

class _AutomationCreationInputViewState
    extends State<AutomationCreationInputView> {
  late String localValue;

  @override
  void initState() {
    super.initState();
    localValue = widget.value ?? "";
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
              value: localValue,
              routeToGoWhenSave: widget.routeToGoWhenSave,
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildInput() {
    switch (widget.type) {
      case AutomationInputEnum.text:
        return _TextInput(
          label: widget.label,
          placeholder: widget.placeholder,
          defaultValue: localValue,
          onValueChanged: (value) {
            setState(() {
              localValue = value;
            });
          },
        );
      case AutomationInputEnum.textArea:
        return _TextAreaInput(
          label: widget.label,
          placeholder: widget.placeholder,
          defaultValue: localValue,
          onChanged: (value) {
            setState(() {
              localValue = value;
            });
          },
        );
      case AutomationInputEnum.radio:
        return _RadioInput(
          label: widget.label,
          options: widget.options!,
          defaultValue: localValue,
          onChanged: (value) {
            setState(() {
              localValue = value;
            });
          },
        );
      default:
        return Container();
    }
  }
}

class _TextInput extends StatefulWidget {
  final String label;
  final String? placeholder;
  final void Function(String) onValueChanged;
  final String defaultValue;

  const _TextInput({
    required this.label,
    this.placeholder,
    required this.onValueChanged,
    required this.defaultValue,
  });

  @override
  State<_TextInput> createState() => _TextInputState();
}

class _TextInputState extends State<_TextInput> {
  late TextEditingController _controller;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController(text: widget.defaultValue);
    _controller.addListener(() {
      widget.onValueChanged(_controller.text);
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      controller: _controller,
      placeholder: widget.placeholder,
      keyboardType: TextInputType.text,
      backgroundColor: Colors.white,
    );
  }
}

class _TextAreaInput extends StatefulWidget {
  final String label;
  final String? placeholder;
  final String? defaultValue;
  final void Function(String)? onChanged;

  const _TextAreaInput({
    required this.label,
    this.placeholder,
    this.defaultValue,
    this.onChanged,
  });

  @override
  State<_TextAreaInput> createState() => _TextAreaInputState();
}

class _TextAreaInputState extends State<_TextAreaInput> {
  late TextEditingController _controller;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController(text: widget.defaultValue);
    _controller.addListener(() {
      if (widget.onChanged != null) {
        widget.onChanged!(_controller.text);
      }
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        if (widget.label.isNotEmpty)
          Padding(
            padding: const EdgeInsets.only(bottom: 8.0),
            child: Text(
              widget.label,
              style: Theme.of(context).textTheme.labelMedium?.copyWith(
                    fontWeight: FontWeight.bold,
                  ),
            ),
          ),
        TriggoInput(
          controller: _controller,
          placeholder: widget.placeholder,
          keyboardType: TextInputType.multiline,
          padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
          backgroundColor: Colors.white,
          maxLines: 5,
        ),
      ],
    );
  }
}

class _OKButton extends StatelessWidget {
  final void Function(String) onSave;
  final String value;
  final String routeToGoWhenSave;

  const _OKButton({
    required this.onSave,
    required this.value,
    required this.routeToGoWhenSave,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Expanded(
          child: TriggoButton(
            text: "OK",
            onPressed: () {
              onSave(value);
              if (routeToGoWhenSave ==
                  RoutesNames.automationTriggerActionRestricted) {
                Navigator.of(context).pop();
              } else if (routeToGoWhenSave ==
                  RoutesNames.automationTriggerActionCustom) {
                Navigator.of(context)
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
    );
  }
}

class _RadioInput extends StatefulWidget {
  final String label;
  final List<AutomationRadioModel> options;
  final void Function(String) onChanged;
  final String defaultValue;

  const _RadioInput({
    required this.label,
    required this.options,
    required this.onChanged,
    required this.defaultValue,
  });

  @override
  State<_RadioInput> createState() => _RadioInputState();
}

class _RadioInputState extends State<_RadioInput> {
  late String localValue;

  @override
  void initState() {
    super.initState();
    localValue = widget.defaultValue;
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        if (widget.label.isNotEmpty)
          Padding(
            padding: const EdgeInsets.only(bottom: 8.0),
            child: Text(
              widget.label,
              style: Theme.of(context).textTheme.labelMedium?.copyWith(
                    fontWeight: FontWeight.bold,
                  ),
            ),
          ),
        for (final option in widget.options)
          RadioListTile<String>(
            title: Text(option.title),
            value: option.value,
            groupValue: localValue,
            onChanged: (value) {
              setState(() {
                localValue = value!;
                widget.onChanged(value);
              });
            },
          ),
      ],
    );
  }
}
