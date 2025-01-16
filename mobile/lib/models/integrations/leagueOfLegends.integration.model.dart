import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/repositories/integration/models/integrations/league_of_legends.integrations.dart';

class LeagueOfLegendsIntegration extends Integration {
  final String riotGameName;
  final String riotTagLine;
  final String summonerProfileIcon;

  LeagueOfLegendsIntegration({
    required super.id,
    required super.name,
    required this.riotGameName,
    required this.riotTagLine,
    required this.summonerProfileIcon,
  });

  static LeagueOfLegendsIntegration fromDTO(IntegrationDTO dto) {
    LeagueOfLegendsPropertiesDTO properties =
        dto.properties as LeagueOfLegendsPropertiesDTO;
    return LeagueOfLegendsIntegration(
      name: 'LeagueOfLegends',
      id: dto.id,
      riotGameName: properties.riotGameName,
      riotTagLine: properties.riotTagLine,
      summonerProfileIcon: properties.summonerProfileIcon,
    );
  }
}
