import 'package:bloc/bloc.dart';
import 'package:triggo/mediator/integrations/integration.mediator.dart';

import 'integrations_event.dart';
import 'integrations_state.dart';

class IntegrationsBloc extends Bloc<IntegrationsEvent, IntegrationsState> {
  final IntegrationMediator _integrationMediator;

  IntegrationsBloc(this._integrationMediator) : super(IntegrationsLoading()) {
    on<LoadIntegrations>(_onLoadIntegrations);
    on<ReloadIntegrations>(_onReloadIntegrations);
  }

  void _onLoadIntegrations(
      LoadIntegrations event, Emitter<IntegrationsState> emit) async {
    emit(IntegrationsLoading());
    try {
      final integrations = await _integrationMediator.getUserIntegrations();
      emit(IntegrationsLoaded(integrations));
    } catch (e) {
      emit(IntegrationsError(e.toString()));
    }
  }

  void _onReloadIntegrations(
      ReloadIntegrations event, Emitter<IntegrationsState> emit) async {
    add(LoadIntegrations());
  }
}
