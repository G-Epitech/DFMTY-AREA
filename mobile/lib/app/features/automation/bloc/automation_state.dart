part of 'automation_bloc.dart';

final class AutomationState extends Equatable {
  final Automation cleanedAutomation;
  final Automation dirtyAutomation;
  final Map<String, String> previews;
  final FormzSubmissionStatus status;

  const AutomationState({
    required this.cleanedAutomation,
    required this.dirtyAutomation,
    required this.previews,
    required this.status,
  });

  @override
  List<Object> get props =>
      [dirtyAutomation, cleanedAutomation, previews, status];

  AutomationState copyWith({
    Automation? cleanedAutomation,
    Automation? dirtyAutomation,
    Map<String, String>? previews,
    FormzSubmissionStatus? status,
  }) {
    return AutomationState(
      cleanedAutomation: cleanedAutomation ?? this.cleanedAutomation,
      dirtyAutomation: dirtyAutomation ?? this.dirtyAutomation,
      previews: previews ?? this.previews,
      status: status ?? this.status,
    );
  }
}

final class AutomationInitial extends AutomationState {
  AutomationInitial()
      : super(
            dirtyAutomation: Automation(
              id: '',
              label: '',
              description: '',
              trigger: AutomationTrigger(
                identifier: '',
                parameters: [],
                dependencies: [],
              ),
              actions: [],
              ownerId: '',
              enabled: true,
              updatedAt: DateTime(0),
              iconColor: 0,
              iconUri: '',
            ),
            cleanedAutomation: Automation(
              id: '',
              label: '',
              description: '',
              trigger: AutomationTrigger(
                identifier: '',
                parameters: [],
                dependencies: [],
              ),
              actions: [],
              ownerId: '',
              enabled: true,
              updatedAt: DateTime(0),
              iconColor: 0,
              iconUri: '',
            ),
            previews: {},
            status: FormzSubmissionStatus.initial);
}
