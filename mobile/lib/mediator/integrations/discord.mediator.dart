import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/app/features/automation/models/radio.model.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/repositories/integration/integrations/discord.repository.dart';

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
      throw Exception('Error getting guilds: $e');
    }
  }

  Future<List<DiscordChannel>> getChannels(String id, String guildId) async {
    List<DiscordChannel> channels = [];
    try {
      final res = await _discordRepository.getChannels(id, guildId);
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.list) {
          channels.add(DiscordChannel.fromDTO(integration));
        }

        return channels;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      throw Exception('Error getting channels: $e');
    }
  }

  Future<List<AutomationRadioModel>> getGuildsRadio(String id) async {
    List<AutomationRadioModel> guildsRadio = [];
    try {
      final guilds = await getGuilds(id);
      for (var guild in guilds) {
        if (guild.linked) {
          guildsRadio.add(AutomationRadioModel(
            title: guild.name,
            description: "Correctly linked",
            value: guild.id,
          ));
        }
      }
      return guildsRadio;
    } catch (e) {
      throw Exception('Error getting guilds: $e');
    }
  }

  Future<List<AutomationRadioModel>> getChannelsRadio(
      String id, String guildId) async {
    List<AutomationRadioModel> channelsRadio = [];
    try {
      final channels = await getChannels(id, guildId);
      for (var channel in channels) {
        channelsRadio.add(AutomationRadioModel(
          title: channel.name,
          description: "",
          value: channel.id,
        ));
      }
      return channelsRadio;
    } catch (e) {
      throw Exception('Error getting channels: $e');
    }
  }
}
