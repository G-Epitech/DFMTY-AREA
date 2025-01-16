part of 'automation_bloc.dart';

sealed class AutomationEvent extends Equatable {
  const AutomationEvent();

  @override
  List<Object> get props => [];
}

final class AutomationSubmitted extends AutomationEvent {
  const AutomationSubmitted();
}

final class AutomationLabelChanged extends AutomationEvent {
  final String label;

  const AutomationLabelChanged({
    required this.label,
  });

  @override
  List<Object> get props => [label];
}

final class AutomationDescriptionChanged extends AutomationEvent {
  final String description;

  const AutomationDescriptionChanged({
    required this.description,
  });

  @override
  List<Object> get props => [description];
}

final class AutomationResetPending extends AutomationEvent {
  final AutomationChoiceEnum type;
  final int index;

  const AutomationResetPending({
    required this.type,
    required this.index,
  });

  @override
  List<Object> get props => [type, index];
}

final class AutomationTriggerProviderAdded extends AutomationEvent {
  final String provider;

  const AutomationTriggerProviderAdded({
    required this.provider,
  });

  @override
  List<Object> get props => [provider];
}

final class AutomationTriggerIdentifierChanged extends AutomationEvent {
  final String identifier;

  const AutomationTriggerIdentifierChanged({
    required this.identifier,
  });

  @override
  List<Object> get props => [identifier];
}

final class AutomationTriggerParameterChanged extends AutomationEvent {
  final String parameterIdentifier;
  final String parameterValue;

  const AutomationTriggerParameterChanged({
    required this.parameterIdentifier,
    required this.parameterValue,
  });

  @override
  List<Object> get props => [parameterIdentifier, parameterValue];
}

final class AutomationActionProviderAdded extends AutomationEvent {
  final int index;
  final String provider;

  const AutomationActionProviderAdded({
    required this.index,
    required this.provider,
  });

  @override
  List<Object> get props => [index, provider];
}

final class AutomationActionIdentifierChanged extends AutomationEvent {
  final int index;
  final String identifier;

  const AutomationActionIdentifierChanged({
    required this.index,
    required this.identifier,
  });

  @override
  List<Object> get props => [index, identifier];
}

final class AutomationActionParameterChanged extends AutomationEvent {
  final int index;
  final String parameterIdentifier;
  final String parameterValue;
  final String parameterType;

  const AutomationActionParameterChanged({
    required this.index,
    required this.parameterIdentifier,
    required this.parameterValue,
    required this.parameterType,
  });

  @override
  List<Object> get props =>
      [index, parameterIdentifier, parameterValue, parameterType];
}

final class AutomationActionDeleted extends AutomationEvent {
  final int index;

  const AutomationActionDeleted({
    required this.index,
  });

  @override
  List<Object> get props => [index];
}

final class AutomationReset extends AutomationEvent {
  const AutomationReset();
}

final class AutomationPreviewUpdated extends AutomationEvent {
  final String key;
  final String value;

  const AutomationPreviewUpdated({
    required this.key,
    required this.value,
  });

  @override
  List<Object> get props => [key, value];
}

final class AutomationValidatePendingAutomation extends AutomationEvent {
  const AutomationValidatePendingAutomation();
}

final class AutomationLoadCleanAutomation extends AutomationEvent {
  const AutomationLoadCleanAutomation();
}
