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

final class AutomationIconChanged extends AutomationEvent {
  final String iconUri;

  const AutomationIconChanged({
    required this.iconUri,
  });

  @override
  List<Object> get props => [iconUri];
}

final class AutomationColorChanged extends AutomationEvent {
  final String color;

  const AutomationColorChanged({
    required this.color,
  });

  @override
  List<Object> get props => [color];
}

final class AutomationResetPending extends AutomationEvent {
  final AutomationTriggerOrActionType type;
  final int index;

  const AutomationResetPending({
    required this.type,
    required this.index,
  });

  @override
  List<Object> get props => [type, index];
}

final class AutomationTriggerDependenciesUpdated extends AutomationEvent {
  final List<String> dependencies;

  const AutomationTriggerDependenciesUpdated({
    required this.dependencies,
  });

  @override
  List<Object> get props => [dependencies];
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

final class AutomationActionDependenciesUpdated extends AutomationEvent {
  final int index;
  final List<String> dependencies;

  const AutomationActionDependenciesUpdated({
    required this.index,
    required this.dependencies,
  });

  @override
  List<Object> get props => [index, dependencies];
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

final class AutomationLoadExisting extends AutomationEvent {
  final Automation automation;

  const AutomationLoadExisting({
    required this.automation,
  });

  @override
  List<Object> get props => [automation];
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

final class AutomationLoadDirtyToClean extends AutomationEvent {
  const AutomationLoadDirtyToClean();
}

final class AutomationLoadCleanToDirty extends AutomationEvent {
  const AutomationLoadCleanToDirty();
}

final class DeleteAutomation extends AutomationEvent {
  final String id;

  const DeleteAutomation(this.id);

  @override
  List<Object> get props => [id];
}
