part of 'league_of_legends_bloc.dart';

sealed class LeagueOfLegendsEvent extends Equatable {
  const LeagueOfLegendsEvent();

  @override
  List<Object> get props => [];
}

final class LeagueOfLegendsGameNameChanged extends LeagueOfLegendsEvent {
  const LeagueOfLegendsGameNameChanged(this.gameName);

  final String gameName;

  @override
  List<Object> get props => [gameName];
}

final class LeagueOfLegendsTagLineChanged extends LeagueOfLegendsEvent {
  const LeagueOfLegendsTagLineChanged(this.tagLine);

  final String tagLine;

  @override
  List<Object> get props => [tagLine];
}

final class LeagueOfLegendsSubmitted extends LeagueOfLegendsEvent {
  const LeagueOfLegendsSubmitted();
}

final class LeagueOfLegendsReset extends LeagueOfLegendsEvent {
  const LeagueOfLegendsReset();
}
