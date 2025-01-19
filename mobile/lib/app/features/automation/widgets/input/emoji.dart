import 'package:flutter/material.dart';
import 'package:triggo/app/widgets/input.triggo.dart';

class EmojiInput extends StatefulWidget {
  final String label;
  final String placeholder;
  final void Function(String) onValueChanged;
  final String defaultValue;

  const EmojiInput({
    super.key,
    required this.label,
    required this.placeholder,
    required this.onValueChanged,
    required this.defaultValue,
  });

  @override
  State<EmojiInput> createState() => EmojiInputState();
}

class EmojiInputState extends State<EmojiInput> {
  late TextEditingController _controller;

  bool isSingleEmoji(String input) {
    final RegExp regexEmoji = RegExp(
        r'^(\u00a9|\u00ae|[\u2000-\u3300]|\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff])$');
    return regexEmoji.hasMatch(input);
  }

  void _validateInput(String input) {
    final graphemes = input.characters.toList();

    if (input.isEmpty) {
      widget.onValueChanged(input);
      return;
    }

    if (graphemes.length == 1 && isSingleEmoji(graphemes.first)) {
      widget.onValueChanged(input);
    } else {
      ScaffoldMessenger.of(context)
        ..removeCurrentSnackBar()
        ..showSnackBar(SnackBar(
          content: const Text('Please enter a single emoji'),
        ));
      _controller.text = widget.defaultValue;
      _controller.selection =
          TextSelection.collapsed(offset: _controller.text.characters.length);
    }
  }

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController(text: widget.defaultValue);

    _controller.addListener(() {
      final input = _controller.text;
      final graphemes = input.characters.toList();

      if (graphemes.length > 1) {
        _controller.text = graphemes.first;
        _controller.selection =
            TextSelection.collapsed(offset: _controller.text.characters.length);
      } else {
        _validateInput(input);
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
      keyboardType: TextInputType.text,
      backgroundColor: Colors.white,
    );
  }
}
