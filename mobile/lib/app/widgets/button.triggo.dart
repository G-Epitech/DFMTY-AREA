import 'package:flutter/material.dart';
import 'package:triggo/app/theme/fonts/fonts.dart';

class TriggoButton extends StatelessWidget {
  final String text;
  final VoidCallback? onPressed;
  final EdgeInsets padding;

  const TriggoButton({
    super.key,
    required this.text,
    this.onPressed,
    this.padding = const EdgeInsets.symmetric(horizontal: 32.0, vertical: 16.0),
  });

  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      onPressed: onPressed,
      style: ElevatedButton.styleFrom(
        backgroundColor: Theme.of(context).colorScheme.primary,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(12.0),
        ),
        padding: padding,
      ),
      child: Text(
        text,
        style: TextStyle(
          color: Theme.of(context).colorScheme.onPrimary,
          fontFamily: containerTitle.fontFamily,
          fontSize: subTitle.fontSize,
          fontWeight: containerTitle.fontWeight,
          letterSpacing: containerTitle.letterSpacing,
        ),
      ),
    );
  }
}
