part of 'automation_creation_bloc.dart';

class AutomationCreationState extends Equatable {
  final Automation cleanedAutomation;
  final Automation dirtyAutomation;
  final Map<String, String> previews;
  final FormzSubmissionStatus status;

  const AutomationCreationState(
    this.cleanedAutomation,
    this.dirtyAutomation,
    this.previews,
    this.status,
  );

  @override
  List<Object> get props => [dirtyAutomation, cleanedAutomation, previews];
}

final class AutomationCreationInitial extends AutomationCreationState {
  AutomationCreationInitial()
      : super(
            Automation(
              id: '',
              label: '',
              description: '',
              trigger: AutomationTrigger(
                identifier: '',
                parameters: [],
                providers: [],
              ),
              actions: [],
              ownerId: '',
              enabled: true,
              updatedAt: DateTime(0),
              iconColor: 0,
              iconUri: '',
            ),
            Automation(
              id: '',
              label: '',
              description: '',
              trigger: AutomationTrigger(
                identifier: '',
                parameters: [],
                providers: [],
              ),
              actions: [],
              ownerId: '',
              enabled: true,
              updatedAt: DateTime(0),
              iconColor: 0,
              iconUri: '',
            ),
            {},
            FormzSubmissionStatus.initial);
}

final class AutomationCreationDirty extends AutomationCreationState {
  const AutomationCreationDirty(
    super.cleanedAutomation,
    super.dirtyAutomation,
    super.previews,
    super.status,
  );
}
