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

final class AutomationCreationTriggerProviderChanged
    extends AutomationCreationEvent {
  final String triggerName;

  const AutomationCreationTriggerProviderChanged({
    required this.triggerName,
  });

  @override
  List<Object> get props => [triggerName];
}

final class AutomationCreationResetPending extends AutomationCreationEvent {
  final AutomationChoiceEnum type;
  final int index;

  const AutomationCreationResetPending({
    required this.type,
    required this.index,
  });

  @override
  List<Object> get props => [type, index];
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

final class AutomationCreationTriggerIdentifierChanged
    extends AutomationCreationEvent {
  final String identifier;

  const AutomationCreationTriggerIdentifierChanged({
    required this.identifier,
  });

  @override
  List<Object> get props => [identifier];
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

final class AutomationCreationActionDeleted extends AutomationCreationEvent {
  final int index;

  const AutomationCreationActionDeleted({
    required this.index,
  });

  @override
  List<Object> get props => [index];
}

final class AutomationCreationSubmitted extends AutomationCreationEvent {
  const AutomationCreationSubmitted();
}

final class AutomationCreationReset extends AutomationCreationEvent {
  const AutomationCreationReset();
}

final class AutomationCreationPreviewUpdated extends AutomationCreationEvent {
  final String key;
  final String value;

  const AutomationCreationPreviewUpdated({
    required this.key,
    required this.value,
  });

  @override
  List<Object> get props => [key, value];
}
