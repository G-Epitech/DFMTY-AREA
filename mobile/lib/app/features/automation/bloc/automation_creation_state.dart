part of 'automation_creation_bloc.dart';

class AutomationCreationState extends Equatable {
  final Automation cleanedAutomation;
  final Automation dirtyAutomation;
  final Map<String, String> previews;

  const AutomationCreationState(
    this.cleanedAutomation,
    this.dirtyAutomation,
    this.previews,
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
            {});
}

final class AutomationCreationDirty extends AutomationCreationState {
  const AutomationCreationDirty(
    super.cleanedAutomation,
    super.dirtyAutomation,
    super.previews,
  );
}
