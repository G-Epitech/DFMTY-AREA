import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation_trigger_bloc.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/features/automation/widgets/dropdown_container.widget.dart';

class AutomationTriggerFields extends StatelessWidget {
  final List<TriggerPropertiesFields> fields;

  const AutomationTriggerFields({super.key, required this.fields});

  @override
  Widget build(BuildContext context) {
    return fields.isNotEmpty
        ? ListView.builder(
            itemCount: fields.length,
            itemBuilder: (context, index) {
              final currentField = fields[index];

              return Padding(
                padding: const EdgeInsets.only(bottom: 8.0),
                child: DropdownContainer(
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
                ),
              );
            },
          )
        : const Center(child: Text('No fields available'));
  }
}
