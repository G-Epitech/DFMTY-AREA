import 'package:equatable/equatable.dart';
import 'package:triggo/models/integration.model.dart';

abstract class IntegrationsState extends Equatable {
  const IntegrationsState();

  @override
  List<Object> get props => [];
}

class IntegrationsInitial extends IntegrationsState {}

class IntegrationsLoading extends IntegrationsState {}

class IntegrationsLoaded extends IntegrationsState {
  final List<Integration> integrations;

  const IntegrationsLoaded(this.integrations);

  @override
  List<Object> get props => [integrations];
}

class IntegrationsError extends IntegrationsState {
  final String message;

  const IntegrationsError(this.message);

  @override
  List<Object> get props => [message];
}
