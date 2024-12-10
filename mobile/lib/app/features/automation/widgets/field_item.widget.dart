import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/theme/colors/colors.dart';

class DropdownItem extends StatelessWidget {
  final TriggerPropertiesFieldsValues option;

  const DropdownItem({required this.option});

  @override
  Widget build(BuildContext context) {
    return Text(
      option.name,
      style: const TextStyle(
        fontFamily: 'Onest',
        fontSize: 14,
        fontWeight: FontWeight.w500,
        letterSpacing: -0.2,
        color: textSecondaryColor,
      ),
    );
  }
}
