import 'package:equatable/equatable.dart';

abstract class IntegrationsEvent extends Equatable {
  const IntegrationsEvent();

  @override
  List<Object> get props => [];
}

class LoadIntegrations extends IntegrationsEvent {}

class ReloadIntegrations extends IntegrationsEvent {}
