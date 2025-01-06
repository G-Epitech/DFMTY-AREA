part of 'automation_trigger_bloc.dart';

sealed class AutomationTriggerEvent extends Equatable {
  const AutomationTriggerEvent();

  @override
  List<Object> get props => [];
}

final class AutomationTriggerPropertiesFieldsSelectedValueChanged
    extends AutomationTriggerEvent {
  final String selectedValue;
  final String name;

  const AutomationTriggerPropertiesFieldsSelectedValueChanged({
    required this.selectedValue,
    required this.name,
  });

  @override
  List<Object> get props => [selectedValue, name];
}
