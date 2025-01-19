import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class TextInput extends StatefulWidget {
  final String? placeholder;
  final void Function(String) onValueChanged;
  final String defaultValue;

  const TextInput({
    super.key,
    this.placeholder,
    required this.onValueChanged,
    required this.defaultValue,
  });

  @override
  State<TextInput> createState() => TextInputState();
}

class TextInputState extends State<TextInput> {
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
