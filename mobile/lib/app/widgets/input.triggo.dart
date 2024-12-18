import 'package:flutter/material.dart';

class TriggoInput extends StatelessWidget {
  final String? placeholder;
  final TextEditingController? controller;
  final TextInputType keyboardType;
  final Function(String)? onChanged;
  final EdgeInsetsGeometry padding;
  final bool obscureText;

  const TriggoInput({
    super.key,
    this.placeholder,
    this.controller,
    this.keyboardType = TextInputType.text,
    this.onChanged,
    this.padding = const EdgeInsets.symmetric(horizontal: 16.0, vertical: 12.0),
    this.obscureText = false,
  });

  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: controller,
      keyboardType: keyboardType,
      obscureText: obscureText,
      onChanged: onChanged,
      style: TextStyle(
        fontSize: 16.0,
        fontFamily: Theme.of(context).textTheme.labelMedium!.fontFamily,
        fontWeight: FontWeight.normal,
        color: Colors.black,
      ),
      decoration: InputDecoration(
        hintText: placeholder,
        hintStyle: TextStyle(
          fontSize: 16.0,
          fontFamily: Theme.of(context).textTheme.labelMedium!.fontFamily,
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
        contentPadding: padding,
      ),
    );
  }
}
