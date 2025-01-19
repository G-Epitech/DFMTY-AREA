import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class TextAreaInput extends StatefulWidget {
  final String? placeholder;
  final String? defaultValue;
  final void Function(String)? onChanged;

  const TextAreaInput({
    super.key,
    this.placeholder,
    this.defaultValue,
    this.onChanged,
  });

  @override
  State<TextAreaInput> createState() => TextAreaInputState();
}

class TextAreaInputState extends State<TextAreaInput> {
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
    return TriggoInput(
      controller: _controller,
      placeholder: widget.placeholder,
      keyboardType: TextInputType.multiline,
      padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
      backgroundColor: Colors.white,
      maxLines: 5,
    );
  }
}
