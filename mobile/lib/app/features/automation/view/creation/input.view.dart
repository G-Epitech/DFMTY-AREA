import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/widgets/input.triggo.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationCreationInputView extends StatefulWidget {
  final AutomationInputEnum type;
  final String label;
  final String? placeholder;
  final List<String>? options;
  final void Function(String) onValueChanged;
  final String? value;

  const AutomationCreationInputView({
    super.key,
    required this.type,
    required this.label,
    this.placeholder,
    this.options,
    required this.onValueChanged,
    this.value,
  });

  @override
  State<AutomationCreationInputView> createState() =>
      _AutomationCreationInputViewState();
}

class _AutomationCreationInputViewState
    extends State<AutomationCreationInputView> {
  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: 'Edit ${widget.label}',
      getBack: true,
      body: Padding(
        padding: const EdgeInsets.all(4.0),
        child: Column(
          children: [
            _buildInput(),
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
          onValueChanged: widget.onValueChanged,
          defaultValue: widget.value,
        );
      case AutomationInputEnum.textArea:
        return _TextAreaInput(
          label: widget.label,
          placeholder: widget.placeholder,
          onValueChanged: widget.onValueChanged,
          defaultValue: widget.value,
        );
      case AutomationInputEnum.select:
        return _SelectInput(
          label: widget.label,
          options: widget.options!,
          onValueChanged: widget.onValueChanged,
        );
      default:
        return Container();
    }
  }
}

class _TextInput extends StatelessWidget {
  final String label;
  final String? placeholder;
  final void Function(String)? onValueChanged;
  final String? defaultValue;

  const _TextInput({
    required this.label,
    this.placeholder,
    this.onValueChanged,
    this.defaultValue,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      defaultValue: defaultValue,
      placeholder: placeholder,
      keyboardType: TextInputType.text,
      padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
      backgroundColor: Colors.white,
      onChanged: onValueChanged,
    );
  }
}

class _TextAreaInput extends StatelessWidget {
  final String label;
  final String? placeholder;
  final void Function(String)? onValueChanged;
  final String? defaultValue;

  const _TextAreaInput({
    required this.label,
    this.placeholder,
    this.onValueChanged,
    this.defaultValue,
  });

  @override
  Widget build(BuildContext context) {
    return TriggoInput(
      defaultValue: defaultValue,
      placeholder: placeholder,
      keyboardType: TextInputType.multiline,
      padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
      backgroundColor: Colors.white,
      maxLines: 5,
      onChanged: onValueChanged,
    );
  }
}

class _SelectInput extends StatelessWidget {
  final String label;
  final List<String> options;
  final void Function(String)? onValueChanged;

  const _SelectInput({
    required this.label,
    required this.options,
    this.onValueChanged,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text(label),
        DropdownButton<String>(
          items: options
              .map((option) => DropdownMenuItem(
                    value: option,
                    child: Text(option),
                  ))
              .toList(),
          onChanged: (value) {
            if (value != null && onValueChanged != null) {
              onValueChanged!(value);
            }
          },
        ),
      ],
    );
  }
}
