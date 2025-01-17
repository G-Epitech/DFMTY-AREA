import 'package:equatable/equatable.dart';

abstract class IntegrationsEvent extends Equatable {
  const IntegrationsEvent();

  @override
  List<Object> get props => [];
}

class LoadIntegrations extends IntegrationsEvent {
  final List<String>? integrationIdentifier;

  const LoadIntegrations({this.integrationIdentifier});
}

class ReloadIntegrations extends IntegrationsEvent {}
