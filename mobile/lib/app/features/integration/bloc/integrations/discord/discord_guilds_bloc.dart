import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:triggo/mediator/integrations/discord.mediator.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';

part 'discord_guilds_event.dart';
part 'discord_guilds_state.dart';

class DiscordGuildsBloc extends Bloc<DiscordGuildsEvent, DiscordGuildsState> {
  final DiscordMediator _discordMediator;

  DiscordGuildsBloc(this._discordMediator) : super(DiscordGuildsInitial()) {
    on<LoadDiscordGuilds>(_onDiscordGuildsEvent);
  }

  void _onDiscordGuildsEvent(
      LoadDiscordGuilds event, Emitter<DiscordGuildsState> emit) async {
    emit(DiscordGuildsLoading());
    try {
      final discordGuilds = await _discordMediator.getGuilds(event.id);
      print(discordGuilds);
      emit(DiscordGuildsLoaded(discordGuilds));
    } catch (e) {
      emit(DiscordGuildsError(e.toString()));
    }
  }
}
