import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

part 'automations_event.dart';
part 'automations_state.dart';

class AutomationsBloc extends Bloc<AutomationsEvent, AutomationsState> {
  final AutomationMediator automationMediator;

  AutomationsBloc({required this.automationMediator})
      : super(AutomationsLoading()) {
    on<LoadAutomations>(_onLoadAutomations);
    on<ReloadAutomations>(_onReloadAutomations);
  }

  void _onLoadAutomations(
      LoadAutomations event, Emitter<AutomationsState> emit) async {
    emit(AutomationsLoading());
    try {
      final automations = await automationMediator.getUserAutomations();
      print('Automations: $automations');
      emit(AutomationsLoaded(automations: automations));
    } catch (e) {
      emit(AutomationsError(message: e.toString()));
    }
  }

  void _onReloadAutomations(
      ReloadAutomations event, Emitter<AutomationsState> emit) async {
    add(LoadAutomations());
  }
}
