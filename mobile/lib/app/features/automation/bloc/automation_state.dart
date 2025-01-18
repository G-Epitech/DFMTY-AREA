part of 'automation_bloc.dart';

final class AutomationState extends Equatable {
  final Automation cleanedAutomation;
  final Automation dirtyAutomation;
  final Map<String, String> previews;
  final FormzSubmissionStatus savingStatus;
  final FormzSubmissionStatus loadingStatus;

  const AutomationState({
    required this.cleanedAutomation,
    required this.dirtyAutomation,
    required this.previews,
    required this.savingStatus,
    required this.loadingStatus,
  });

  @override
  List<Object> get props => [
        dirtyAutomation,
        cleanedAutomation,
        previews,
        savingStatus,
        loadingStatus
      ];

  AutomationState copyWith({
    Automation? cleanedAutomation,
    Automation? dirtyAutomation,
    Map<String, String>? previews,
    FormzSubmissionStatus? savingStatus,
    FormzSubmissionStatus? loadingStatus,
  }) {
    return AutomationState(
      cleanedAutomation: cleanedAutomation ?? this.cleanedAutomation,
      dirtyAutomation: dirtyAutomation ?? this.dirtyAutomation,
      previews: previews ?? this.previews,
      savingStatus: savingStatus ?? this.savingStatus,
      loadingStatus: loadingStatus ?? this.loadingStatus,
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
            savingStatus: FormzSubmissionStatus.initial,
            loadingStatus: FormzSubmissionStatus.initial);
}
