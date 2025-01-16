import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/app/features/automation/models/radio.model.dart';
import 'package:triggo/models/integrations/notion.integration.model.dart';
import 'package:triggo/repositories/integration/notion.repository.dart';

class NotionMediator with ChangeNotifier {
  final NotionRepository _notionRepository;

  NotionMediator(this._notionRepository);

  Future<List<NotionDatabase>> getDatabases(String id) async {
    List<NotionDatabase> databases = [];
    try {
      final res = await _notionRepository.getDatabases(id);
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.list) {
          databases.add(NotionDatabase.fromDTO(integration));
        }

        return databases;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      throw Exception('Error getting databases: $e');
    }
  }

  Future<List<NotionPage>> getPages(String id) async {
    List<NotionPage> channels = [];
    try {
      final res = await _notionRepository.getPages(id);
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.list) {
          channels.add(NotionPage.fromDTO(integration));
        }

        return channels;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      throw Exception('Error getting channels: $e');
    }
  }

  Future<List<AutomationRadioModel>> getDatabasesRadio(String id) async {
    List<AutomationRadioModel> guildsRadio = [];
    try {
      final databases = await getDatabases(id);
      for (var database in databases) {
        guildsRadio.add(AutomationRadioModel(
          title: database.title,
          description: database.description ?? "Database",
          value: database.id,
        ));
      }
      return guildsRadio;
    } catch (e) {
      throw Exception('Error getting databases: $e');
    }
  }

  Future<List<AutomationRadioModel>> getPagesRadio(String id) async {
    List<AutomationRadioModel> channelsRadio = [];
    try {
      final channels = await getPages(id);
      for (var channel in channels) {
        channelsRadio.add(AutomationRadioModel(
          title: channel.title,
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
