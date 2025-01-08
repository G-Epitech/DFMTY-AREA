import 'package:flutter/material.dart';

class TriggoInput extends StatelessWidget {
  final String? placeholder;
  final TextInputType keyboardType;
  final Function(String)? onChanged;
  final EdgeInsetsGeometry padding;
  final bool obscureText;
  final Color? color;
  final Color? backgroundColor;
  final int? maxLines;
  final String? defaultValue;

  const TriggoInput({
    super.key,
    this.placeholder,
    this.keyboardType = TextInputType.text,
    this.onChanged,
    this.padding = const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
    this.obscureText = false,
    this.color,
    this.backgroundColor,
    this.maxLines,
    this.defaultValue,
  });

  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: TextEditingController(text: defaultValue),
      keyboardType: keyboardType,
      obscureText: obscureText,
      onChanged: onChanged,
      maxLines: maxLines,
      style: TextStyle(
        fontSize: 16.0,
        fontFamily: Theme.of(context).textTheme.labelMedium!.fontFamily,
        fontWeight: FontWeight.normal,
        color: Colors.black,
      ),
      decoration: InputDecoration(
        hintText: placeholder,
        filled: true,
        fillColor: backgroundColor,
        hintStyle: TextStyle(
          fontSize: 16.0,
          fontFamily: Theme.of(context).textTheme.labelMedium!.fontFamily,
          fontWeight: FontWeight.normal,
          color: Colors.grey,
          backgroundColor: backgroundColor,
        ),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8.0),
          borderSide: BorderSide(color: Colors.grey.shade100),
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8.0),
          borderSide: BorderSide(color: Colors.grey.shade900),
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8.0),
          borderSide: BorderSide(color: Colors.grey[200]!),
        ),
        contentPadding: padding,
      ),
    );
  }
}
