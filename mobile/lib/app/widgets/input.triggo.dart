import 'package:flutter/material.dart';

class TriggoInput extends StatelessWidget {
  final String? placeholder;
  final TextInputType keyboardType;
  final TextEditingController? controller;
  final Function(String)? onChanged;
  final EdgeInsetsGeometry padding;
  final bool obscureText;
  final Color? color;
  final Color? backgroundColor;
  final int? maxLines;

  const TriggoInput({
    super.key,
    this.placeholder,
    this.keyboardType = TextInputType.text,
    this.controller,
    this.onChanged,
    this.padding = const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
    this.obscureText = false,
    this.color,
    this.backgroundColor,
    this.maxLines,
  });

  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: controller,
      keyboardType: keyboardType,
      obscureText: obscureText,
      onChanged: onChanged,
      maxLines: maxLines,
      style: TextStyle(
        fontSize: 16.0,
        fontFamily: Theme.of(context).textTheme.labelMedium?.fontFamily,
        fontWeight: FontWeight.normal,
        color: color ?? Colors.black,
      ),
      decoration: InputDecoration(
        hintText: placeholder,
        filled: true,
        fillColor: backgroundColor,
        hintStyle: TextStyle(
          fontSize: 16.0,
          fontFamily: Theme.of(context).textTheme.labelMedium?.fontFamily,
          fontWeight: FontWeight.normal,
          color: Colors.grey,
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
