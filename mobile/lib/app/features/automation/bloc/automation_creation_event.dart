part of 'automation_creation_bloc.dart';

@immutable
sealed class AutomationCreationEvent extends Equatable {
  const AutomationCreationEvent();

  @override
  List<Object> get props => [];
}

final class AutomationCreationLabelChanged extends AutomationCreationEvent {
  final String label;

  const AutomationCreationLabelChanged({
    required this.label,
  });

  @override
  List<Object> get props => [label];
}

final class AutomationCreationDescriptionChanged
    extends AutomationCreationEvent {
  final String description;

  const AutomationCreationDescriptionChanged({
    required this.description,
  });

  @override
  List<Object> get props => [description];
}

final class AutomationCreationTriggerNameChanged
    extends AutomationCreationEvent {
  final String triggerName;

  const AutomationCreationTriggerNameChanged({
    required this.triggerName,
  });

  @override
  List<Object> get props => [triggerName];
}

final class AutomationCreationTriggerProviderAdded
    extends AutomationCreationEvent {
  final String provider;

  const AutomationCreationTriggerProviderAdded({
    required this.provider,
  });

  @override
  List<Object> get props => [provider];
}

final class AutomationCreationTriggerParameterChanged
    extends AutomationCreationEvent {
  final String parameterIdentifier;
  final String parameterValue;

  const AutomationCreationTriggerParameterChanged({
    required this.parameterIdentifier,
    required this.parameterValue,
  });

  @override
  List<Object> get props => [parameterIdentifier, parameterValue];
}

final class AutomationCreationActionProviderAdded
    extends AutomationCreationEvent {
  final int index;
  final String provider;

  const AutomationCreationActionProviderAdded({
    required this.index,
    required this.provider,
  });

  @override
  List<Object> get props => [index, provider];
}

final class AutomationCreationActionParameterChanged
    extends AutomationCreationEvent {
  final int index;
  final String parameterIdentifier;
  final String parameterValue;
  final String parameterType;

  const AutomationCreationActionParameterChanged({
    required this.index,
    required this.parameterIdentifier,
    required this.parameterValue,
    required this.parameterType,
  });

  @override
  List<Object> get props =>
      [index, parameterIdentifier, parameterValue, parameterType];
}

final class AutomationCreationSubmitted extends AutomationCreationEvent {
  const AutomationCreationSubmitted();
}
