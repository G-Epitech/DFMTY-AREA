part of 'discord_guilds_bloc.dart';

abstract class DiscordGuildsState extends Equatable {
  const DiscordGuildsState();

  @override
  List<Object> get props => [];
}

class DiscordGuildsInitial extends DiscordGuildsState {}

class DiscordGuildsLoading extends DiscordGuildsState {}

class DiscordGuildsLoaded extends DiscordGuildsState {
  final List<DiscordGuild> guilds;

  const DiscordGuildsLoaded(this.guilds);

  @override
  List<Object> get props => [guilds];
}

class DiscordGuildsError extends DiscordGuildsState {
  final String message;

  const DiscordGuildsError(this.message);

  @override
  List<Object> get props => [message];
}
