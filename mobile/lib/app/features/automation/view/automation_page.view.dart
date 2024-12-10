import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:triggo/app/features/automation/bloc/automation_trigger_bloc.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';

class AutomationScreen extends StatelessWidget {
  final List<TriggerPropertiesFields> initialFields = [
    TriggerPropertiesFields(
      name: 'List Guilds',
      values: [
        TriggerPropertiesFieldsValues(
          name: 'Guild 1',
          id: 'id1',
        ),
        TriggerPropertiesFieldsValues(
          name: 'Guild 2',
          id: 'id2',
        ),
      ],
      selectedValue: 'id1',
    )
  ];

  AutomationScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (_) => AutomationTriggerBloc(
        triggerPropertiesFields: initialFields,
      ),
      child: const _AutomationScreenBody(),
    );
  }
}

class _AutomationScreenBody extends StatelessWidget {
  const _AutomationScreenBody();

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      body: Container(
        decoration: BoxDecoration(
          color: Colors.grey[200],
        ),
        child: Center(
          child: Container(
            margin: const EdgeInsets.symmetric(horizontal: 16.0),
            child: BlocBuilder<AutomationTriggerBloc, AutomationTriggerState>(
              builder: (context, state) {
                return _DropdownExample(
                  fields: state.triggerPropertiesFields,
                );
              },
            ),
          ),
        ),
      ),
    );
  }
}

class _DropdownExample extends StatelessWidget {
  final List<TriggerPropertiesFields> fields;

  const _DropdownExample({required this.fields});

  @override
  Widget build(BuildContext context) {
    final currentField = fields.isNotEmpty ? fields.first : null;

    return currentField != null
        ? DropdownButtonHideUnderline(
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
                      value: currentField.selectedValue,
                      isExpanded: true,
                      items: currentField.values.map((option) {
                        return DropdownMenuItem<String>(
                          value: option.id,
                          child: Text(
                            option.name,
                            style: const TextStyle(
                              fontFamily: 'Onest',
                              fontSize: 14,
                              fontWeight: FontWeight.w500,
                              letterSpacing: -0.2,
                              color: textSecondaryColor,
                            ),
                          ),
                        );
                      }).toList(),
                      borderRadius: BorderRadius.circular(12.0),
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
                      icon: currentField.selectedValue == 'Select Guild'
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
          )
        : const Text('No fields available');
  }
}
