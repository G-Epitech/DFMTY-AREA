part of 'automation_creation_bloc.dart';

class AutomationCreationState extends Equatable {
  final Automation automation;
  final Map<String, String> previews;
  final bool isValid;

  const AutomationCreationState(
    this.automation,
    this.previews,
    this.isValid,
  );

  @override
  List<Object> get props => [automation, previews, isValid];
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
            {},
            false);
}
