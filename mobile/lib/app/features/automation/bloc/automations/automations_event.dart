part of 'automations_bloc.dart';

sealed class AutomationsEvent extends Equatable {
  const AutomationsEvent();

  @override
  List<Object> get props => [];
}

final class LoadAutomations extends AutomationsEvent {}

final class ReloadAutomations extends AutomationsEvent {}
