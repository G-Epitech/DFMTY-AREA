import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/utils/json.dart';

part 'leagueOfLegends.dtos.g.dart';

@JsonSerializable()
class InLinkLeagueOfLegendsDTO implements Json {
  final String gameName;
  final String tagLine;

  InLinkLeagueOfLegendsDTO({required this.gameName, required this.tagLine});

  @override
  factory InLinkLeagueOfLegendsDTO.fromJson(Map<String, dynamic> json) =>
      _$InLinkLeagueOfLegendsDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InLinkLeagueOfLegendsDTOToJson(this);
}

class OutLeagueOfLegendsLinkAccountDTO implements Json {
  OutLeagueOfLegendsLinkAccountDTO();

  @override
  factory OutLeagueOfLegendsLinkAccountDTO.fromJson(Map<String, dynamic> json) {
    return OutLeagueOfLegendsLinkAccountDTO();
  }

  @override
  Map<String, dynamic> toJson() {
    return {};
  }
}
