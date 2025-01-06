import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:triggo/app/features/automation/models/trigger_properties_fields.dart';

part 'automation_trigger_event.dart';
part 'automation_trigger_state.dart';

class AutomationTriggerBloc
    extends Bloc<AutomationTriggerEvent, AutomationTriggerState> {
  AutomationTriggerBloc(
      {required List<TriggerPropertiesFields> triggerPropertiesFields})
      : super(AutomationTriggerInitial(triggerPropertiesFields)) {
    on<AutomationTriggerPropertiesFieldsSelectedValueChanged>(
        _onTriggerPropertiesFieldsSelectedValueChanged);
  }

  void _onTriggerPropertiesFieldsSelectedValueChanged(
    AutomationTriggerPropertiesFieldsSelectedValueChanged event,
    Emitter<AutomationTriggerState> emit,
  ) {
    final triggerPropertiesFields = state.triggerPropertiesFields.map((e) {
      if (e.name == event.name) {
        return e.copyWith(selectedValue: event.selectedValue);
      }
      return e;
    }).toList();
    emit(AutomationTriggerInitial(triggerPropertiesFields));
  }
}
