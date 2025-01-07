part of 'discord_guilds_bloc.dart';

abstract class DiscordGuildsEvent extends Equatable {
  const DiscordGuildsEvent();

  @override
  List<Object> get props => [];
}

class LoadDiscordGuilds extends DiscordGuildsEvent {
  final String id;

  const LoadDiscordGuilds(this.id);

  @override
  List<Object> get props => [id];
}
