import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation_trigger_bloc.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';
import 'package:triggo/app/features/automation/widgets/field.widget.dart';
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
    ),
    TriggerPropertiesFields(
      name: 'List Channels',
      values: [
        TriggerPropertiesFieldsValues(
          name: 'Channel 1',
          id: 'id1',
        ),
        TriggerPropertiesFieldsValues(
          name: 'Channel 2',
          id: 'id2',
        ),
      ],
    ),
  ];

  AutomationScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (_) => AutomationTriggerBloc(
        triggerPropertiesFields: initialFields,
      ),
      child: const AutomationScreenBody(),
    );
  }
}

class AutomationScreenBody extends StatelessWidget {
  const AutomationScreenBody();

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
                return DropdownExample(
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
