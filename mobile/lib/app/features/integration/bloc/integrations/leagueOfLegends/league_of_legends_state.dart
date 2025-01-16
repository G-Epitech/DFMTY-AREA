part of 'league_of_legends_bloc.dart';

final class LeagueOfLegendsIntegrationState extends Equatable {
  const LeagueOfLegendsIntegrationState({
    this.status = FormzSubmissionStatus.initial,
    this.gameName = const GameName.pure(),
    this.tagLine = const TagLine.pure(),
    this.isValid = false,
  });

  final FormzSubmissionStatus status;
  final GameName gameName;
  final TagLine tagLine;
  final bool isValid;

  LeagueOfLegendsIntegrationState copyWith({
    FormzSubmissionStatus? status,
    GameName? gameName,
    TagLine? tagLine,
    bool? isValid,
  }) {
    return LeagueOfLegendsIntegrationState(
      status: status ?? this.status,
      gameName: gameName ?? this.gameName,
      tagLine: tagLine ?? this.tagLine,
      isValid: isValid ?? this.isValid,
    );
  }

  @override
  List<Object> get props => [status, gameName, tagLine];
}
