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
      final res = await _discordRepository.getGuilds(id);
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
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }
}
