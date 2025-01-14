import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/utils/json.dart';

part 'openAI.dtos.g.dart';

@JsonSerializable()
class InLinkOpenAIDTO implements Json {
  final String apiToken;
  final String adminApiToken;

  InLinkOpenAIDTO({required this.apiToken, required this.adminApiToken});

  @override
  factory InLinkOpenAIDTO.fromJson(Map<String, dynamic> json) =>
      _$InLinkOpenAIDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InLinkOpenAIDTOToJson(this);
}

class OutOpenAILinkAccountDTO implements Json {
  OutOpenAILinkAccountDTO();

  @override
  factory OutOpenAILinkAccountDTO.fromJson(Map<String, dynamic> json) {
    return OutOpenAILinkAccountDTO();
  }

  @override
  Map<String, dynamic> toJson() {
    return {};
  }
}
