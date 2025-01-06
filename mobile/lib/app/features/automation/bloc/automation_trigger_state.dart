part of 'automation_trigger_bloc.dart';

sealed class AutomationTriggerState extends Equatable {
  final List<TriggerPropertiesFields> triggerPropertiesFields;

  const AutomationTriggerState(
    this.triggerPropertiesFields,
  );
}

final class AutomationTriggerInitial extends AutomationTriggerState {
  const AutomationTriggerInitial(super.triggerPropertiesFields);

  @override
  List<Object> get props => [triggerPropertiesFields];
}
