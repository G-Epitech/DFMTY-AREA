import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/features/automation/widgets/field_item.widget.dart';
import 'package:triggo/app/theme/colors/colors.dart';

class DropdownContainer extends StatelessWidget {
  final TriggerPropertiesFields field;
  final ValueChanged<String?> onChanged;

  const DropdownContainer({
    required this.field,
    required this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    return DropdownButtonHideUnderline(
      child: Container(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(12.0),
          color: Theme.of(context).colorScheme.surface,
        ),
        child: Row(
          children: [
            SvgPicture.asset(
              'assets/icons/people.svg',
              colorFilter: const ColorFilter.mode(
                textSecondaryColor,
                BlendMode.srcIn,
              ),
              width: 18,
            ),
            const SizedBox(width: 8.0),
            Expanded(
              child: DropdownButton<String>(
                value: field.selectedValue,
                isExpanded: true,
                items: field.values.map((option) {
                  return DropdownMenuItem(
                    value: option.id,
                    child: DropdownItem(option: option),
                  );
                }).toList(),
                borderRadius: BorderRadius.circular(12.0),
                onChanged: onChanged,
                icon: field.selectedValue == 'Select Guild'
                    ? Icon(Icons.warning_amber_rounded,
                        color: Theme.of(context).colorScheme.onError)
                    : const Icon(Icons.arrow_drop_down,
                        color: textSecondaryColor),
                dropdownColor: Colors.white,
                underline: Container(),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
