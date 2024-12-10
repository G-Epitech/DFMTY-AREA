import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation_trigger_bloc.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/features/automation/widgets/field_container.widget.dart';

class DropdownExample extends StatelessWidget {
  final List<TriggerPropertiesFields> fields;

  const DropdownExample({required this.fields});

  @override
  Widget build(BuildContext context) {
    final currentField = fields.isNotEmpty ? fields.first : null;

    return currentField != null
        ? DropdownContainer(
            field: currentField,
            onChanged: (String? newValue) {
              if (newValue != null) {
                context.read<AutomationTriggerBloc>().add(
                      AutomationTriggerPropertiesFieldsSelectedValueChanged(
                        selectedValue: newValue,
                        name: currentField.name,
                      ),
                    );
              }
            },
          )
        : const Text('No fields available');
  }
}
