import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/repositories/integration/discord.repository.dart';

class DiscordMediator with ChangeNotifier {
  final DiscordRepository _discordRepository;

  DiscordMediator(this._discordRepository);

  Future<List<DiscordGuild>> getGuilds(String id) async {
    List<DiscordGuild> guilds = [];
    try {
      print('Getting guilds');
      final res = await _discordRepository.getGuilds(id);
      print('Got guilds');
      print(res.data);
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.list) {
          guilds.add(DiscordGuild.fromDTO(integration));
        }

        guilds.sort((a, b) {
          if (a.linked && !b.linked) {
            return -1;
          } else if (!a.linked && b.linked) {
            return 1;
          }
          return 0;
        });

        return guilds;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      print('Error getting guilds: $e');
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }
}
