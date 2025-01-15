import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

class LeagueOfLegendsPropertiesDTO implements IntegrationPropertiesDTO {
  final String riotAccountId;
  final String riotGameName;
  final String riotTagLine;
  final String summonerId;
  final String summonerAccountId;
  final String summonerProfileIcon;

  LeagueOfLegendsPropertiesDTO({
    required this.riotAccountId,
    required this.riotGameName,
    required this.riotTagLine,
    required this.summonerId,
    required this.summonerAccountId,
    required this.summonerProfileIcon,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'riotAccountId': riotAccountId,
      'riotGameName': riotGameName,
      'riotTagLine': riotTagLine,
      'summonerId': summonerId,
      'summonerAccountId': summonerAccountId,
      'summonerProfileIcon': summonerProfileIcon,
    };
  }

  factory LeagueOfLegendsPropertiesDTO.fromJson(Map<String, dynamic> json) {
    return LeagueOfLegendsPropertiesDTO(
      riotAccountId: json['riotAccountId'] as String,
      riotGameName: json['riotGameName'] as String,
      riotTagLine: json['riotTagLine'] as String,
      summonerId: json['summonerId'] as String,
      summonerAccountId: json['summonerAccountId'] as String,
      summonerProfileIcon: json['summonerProfileIcon'] as String,
    );
  }
}
