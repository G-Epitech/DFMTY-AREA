part of 'automations_bloc.dart';

sealed class AutomationsState {
  const AutomationsState();

  List<Object> get props => [];
}

final class AutomationsInitial extends AutomationsState {}

final class AutomationsLoading extends AutomationsState {}

final class AutomationsLoaded extends AutomationsState {
  final List<Automation> automations;

  AutomationsLoaded({
    required this.automations,
  });

  @override
  List<Object> get props => [automations];
}

final class AutomationsError extends AutomationsState {
  final String message;

  AutomationsError({
    required this.message,
  });

  @override
  List<Object> get props => [message];
}
