import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/integration/models/models.dart';
import 'package:triggo/mediator/integrations/leagueOfLegends.mediator.dart';

part 'league_of_legends_event.dart';
part 'league_of_legends_state.dart';

class LeagueOfLegendsIntegrationBloc
    extends Bloc<LeagueOfLegendsEvent, LeagueOfLegendsIntegrationState> {
  LeagueOfLegendsIntegrationBloc({
    required LeagueOfLegendsMediator leagueOfLegends,
  })  : _leagueOfLegendsMediator = leagueOfLegends,
        super(const LeagueOfLegendsIntegrationState()) {
    on<LeagueOfLegendsGameNameChanged>(_onApiKeyChanged);
    on<LeagueOfLegendsTagLineChanged>(_onAdminApiKeyChanged);
    on<LeagueOfLegendsSubmitted>(_onSubmitted);
    on<LeagueOfLegendsReset>(_onReset);
  }

  final LeagueOfLegendsMediator _leagueOfLegendsMediator;

  void _onApiKeyChanged(
    LeagueOfLegendsGameNameChanged event,
    Emitter<LeagueOfLegendsIntegrationState> emit,
  ) {
    final gameName = GameName.dirty(event.gameName);
    emit(
      state.copyWith(
        gameName: gameName,
        isValid: Formz.validate([gameName, state.tagLine]),
      ),
    );
  }

  void _onAdminApiKeyChanged(
    LeagueOfLegendsTagLineChanged event,
    Emitter<LeagueOfLegendsIntegrationState> emit,
  ) {
    final tagLine = TagLine.dirty(event.tagLine);
    emit(
      state.copyWith(
        tagLine: tagLine,
        isValid: Formz.validate([tagLine, state.gameName]),
      ),
    );
  }

  Future<void> _onSubmitted(
    LeagueOfLegendsSubmitted event,
    Emitter<LeagueOfLegendsIntegrationState> emit,
  ) async {
    if (state.isValid) {
      emit(state.copyWith(status: FormzSubmissionStatus.inProgress));
      try {
        await _leagueOfLegendsMediator.linkAccount(
          state.gameName.value,
          state.tagLine.value,
        );

        emit(state.copyWith(status: FormzSubmissionStatus.success));
      } catch (_) {
        emit(state.copyWith(status: FormzSubmissionStatus.failure));
      }
    }
  }

  Future<void> _onReset(
    LeagueOfLegendsReset event,
    Emitter<LeagueOfLegendsIntegrationState> emit,
  ) async {
    emit(
      state.copyWith(
        status: FormzSubmissionStatus.initial,
      ),
    );
  }
}
