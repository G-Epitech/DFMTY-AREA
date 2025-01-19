import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/automation.model.dart';

Future<List<AutomationRadioModel>> getParameterOptions(
    Automation automation,
    AutomationTriggerOrActionType type,
    String integrationName,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier,
    IntegrationMediator integrationMediator) async {
  switch (type) {
    case AutomationTriggerOrActionType.trigger:
      return _handleTrigger(
        automation,
        integrationName,
        propertyIdentifier,
        parameterIdentifier,
        integrationMediator,
      );
    case AutomationTriggerOrActionType.action:
      return _handleAction(
        automation,
        integrationName,
        indexOfTheTriggerOrAction,
        propertyIdentifier,
        parameterIdentifier,
        integrationMediator,
      );
  }
}

Future<List<AutomationRadioModel>> _handleTrigger(
  Automation automation,
  String integrationName,
  String propertyIdentifier,
  String parameterIdentifier,
  IntegrationMediator integrationMediator,
) async {
  if (automation.trigger.dependencies.isEmpty) {
    return [];
  }

  if (integrationName == IntegrationNames.discord) {
    return _handleDiscordTrigger(
      automation,
      propertyIdentifier,
      parameterIdentifier,
      integrationMediator,
    );
  }

  if (integrationName == IntegrationNames.notion) {
    return _handleNotionTrigger(
      automation,
      propertyIdentifier,
      parameterIdentifier,
      integrationMediator,
    );
  }

  return [];
}

Future<List<AutomationRadioModel>> _handleDiscordTrigger(
  Automation automation,
  String propertyIdentifier,
  String parameterIdentifier,
  IntegrationMediator integrationMediator,
) async {
  final integrationId = automation.trigger.dependencies[0];

  if (propertyIdentifier == 'MessageReceivedInChannel') {
    if (parameterIdentifier == 'GuildId') {
      return await integrationMediator.discord.getGuildsRadio(integrationId);
    }
    if (parameterIdentifier == 'ChannelId') {
      final guildId = _findTriggerParameterValue(
        automation.trigger.parameters,
        'GuildId',
      );
      if (guildId.isEmpty) {
        return [];
      }
      return await integrationMediator.discord.getChannelsRadio(
        integrationId,
        guildId,
      );
    }
  }

  return [];
}

Future<List<AutomationRadioModel>> _handleNotionTrigger(
  Automation automation,
  String propertyIdentifier,
  String parameterIdentifier,
  IntegrationMediator integrationMediator,
) async {
  final integrationId = automation.trigger.dependencies[0];

  if ((propertyIdentifier == 'DatabaseRowCreated' ||
          propertyIdentifier == 'DatabaseRowDeleted') &&
      parameterIdentifier == 'DatabaseId') {
    return await integrationMediator.notion.getDatabasesRadio(integrationId);
  }

  return [];
}

Future<List<AutomationRadioModel>> _handleAction(
  Automation automation,
  String integrationName,
  int indexOfTheTriggerOrAction,
  String propertyIdentifier,
  String parameterIdentifier,
  IntegrationMediator integrationMediator,
) async {
  final action = automation.actions[indexOfTheTriggerOrAction];

  if (action.dependencies.isEmpty) {
    return [];
  }

  if (integrationName == IntegrationNames.discord) {
    return _handleDiscordAction(
      action,
      propertyIdentifier,
      parameterIdentifier,
      integrationMediator,
    );
  }

  if (integrationName == IntegrationNames.notion) {
    return _handleNotionAction(
      action,
      propertyIdentifier,
      parameterIdentifier,
      integrationMediator,
    );
  }

  return [];
}

Future<List<AutomationRadioModel>> _handleDiscordAction(
  AutomationAction action,
  String propertyIdentifier,
  String parameterIdentifier,
  IntegrationMediator integrationMediator,
) async {
  final integrationId = action.dependencies[0];

  if (propertyIdentifier == 'SendMessageToChannel') {
    if (parameterIdentifier == 'GuildId') {
      return await integrationMediator.discord.getGuildsRadio(integrationId);
    }
    if (parameterIdentifier == 'ChannelId') {
      final guildId = _findActionParameterValue(action.parameters, 'GuildId');
      if (guildId.isEmpty) {
        return [];
      }
      return await integrationMediator.discord.getChannelsRadio(
        integrationId,
        guildId,
      );
    }
  }

  return [];
}

Future<List<AutomationRadioModel>> _handleNotionAction(
  AutomationAction action,
  String propertyIdentifier,
  String parameterIdentifier,
  IntegrationMediator integrationMediator,
) async {
  final integrationId = action.dependencies[0];

  if ((propertyIdentifier == 'CreateDatabase' ||
          propertyIdentifier == 'CreatePage') &&
      parameterIdentifier == 'ParentId') {
    return await integrationMediator.notion.getPagesRadio(integrationId);
  }

  if (propertyIdentifier == 'CreateDatabaseRow' &&
      parameterIdentifier == 'DatabaseId') {
    return await integrationMediator.notion.getDatabasesRadio(integrationId);
  }

  if (propertyIdentifier == 'ArchiveDatabase' &&
      parameterIdentifier == 'DatabaseId') {
    return await integrationMediator.notion.getDatabasesRadio(integrationId);
  }

  if (propertyIdentifier == 'ArchivePage' && parameterIdentifier == 'PageId') {
    return await integrationMediator.notion.getPagesRadio(integrationId);
  }

  return [];
}

String _findTriggerParameterValue(
  List<AutomationTriggerParameter> parameters,
  String identifier,
) {
  for (final parameter in parameters) {
    if (parameter.identifier == identifier) {
      return parameter.value;
    }
  }
  return '';
}

String _findActionParameterValue(
  List<AutomationActionParameter> parameters,
  String identifier,
) {
  for (final parameter in parameters) {
    if (parameter.identifier == identifier) {
      return parameter.value;
    }
  }
  return '';
}
