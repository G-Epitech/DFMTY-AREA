import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/models/automation.model.dart';

AutomationParameterNeedOptions parameterHaveOptions(
    Automation automation,
    AutomationTriggerOrActionType type,
    String integrationName,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier) {
  switch (type) {
    case AutomationTriggerOrActionType.trigger:
      return _handleTriggerOptions(
          automation, integrationName, propertyIdentifier, parameterIdentifier);
    case AutomationTriggerOrActionType.action:
      return _handleActionOptions(automation, integrationName,
          indexOfTheTriggerOrAction, propertyIdentifier, parameterIdentifier);
  }
}

AutomationParameterNeedOptions _handleTriggerOptions(
    Automation automation,
    String integrationName,
    String propertyIdentifier,
    String parameterIdentifier) {
  if (integrationName == IntegrationNames.discord) {
    return _handleDiscordTriggerOptions(
        automation, propertyIdentifier, parameterIdentifier);
  }

  if (integrationName == IntegrationNames.notion) {
    return _handleNotionTriggerOptions(propertyIdentifier, parameterIdentifier);
  }

  if (integrationName == IntegrationNames.leagueOfLegends &&
      parameterIdentifier == "KdaThreshold") {
    return AutomationParameterNeedOptions.number;
  }

  return AutomationParameterNeedOptions.no;
}

AutomationParameterNeedOptions _handleDiscordTriggerOptions(
    Automation automation,
    String propertyIdentifier,
    String parameterIdentifier) {
  if (propertyIdentifier == 'MessageReceivedInChannel') {
    if (parameterIdentifier == 'GuildId') {
      return AutomationParameterNeedOptions.yes;
    }
    if (parameterIdentifier == 'ChannelId') {
      return automation.trigger.parameters.isEmpty
          ? AutomationParameterNeedOptions.blocked
          : AutomationParameterNeedOptions.yes;
    }
  }
  return AutomationParameterNeedOptions.no;
}

AutomationParameterNeedOptions _handleNotionTriggerOptions(
    String propertyIdentifier, String parameterIdentifier) {
  if ((propertyIdentifier == 'DatabaseRowCreated' ||
          propertyIdentifier == 'DatabaseRowDeleted') &&
      parameterIdentifier == 'DatabaseId') {
    return AutomationParameterNeedOptions.yes;
  }
  return AutomationParameterNeedOptions.no;
}

AutomationParameterNeedOptions _handleActionOptions(
    Automation automation,
    String integrationName,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier) {
  if (integrationName == IntegrationNames.discord) {
    return _handleDiscordActionOptions(automation, indexOfTheTriggerOrAction,
        propertyIdentifier, parameterIdentifier);
  }

  if (integrationName == IntegrationNames.notion) {
    return _handleNotionActionOptions(propertyIdentifier, parameterIdentifier);
  }

  return AutomationParameterNeedOptions.no;
}

AutomationParameterNeedOptions _handleDiscordActionOptions(
    Automation automation,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier) {
  if (propertyIdentifier == 'SendMessageToChannel') {
    if (parameterIdentifier == 'GuildId') {
      return AutomationParameterNeedOptions.yes;
    }
    if (parameterIdentifier == 'ChannelId') {
      for (final parameter
          in automation.actions[indexOfTheTriggerOrAction].parameters) {
        if (parameter.identifier == 'GuildId' && parameter.value.isNotEmpty) {
          return AutomationParameterNeedOptions.yes;
        }
      }
      return AutomationParameterNeedOptions.blocked;
    }
  }
  return AutomationParameterNeedOptions.no;
}

AutomationParameterNeedOptions _handleNotionActionOptions(
    String propertyIdentifier, String parameterIdentifier) {
  if (parameterIdentifier == "Icon") {
    return AutomationParameterNeedOptions.yes;
  }

  if ((propertyIdentifier == 'CreateDatabase' ||
          propertyIdentifier == 'CreatePage') &&
      parameterIdentifier == 'ParentId') {
    return AutomationParameterNeedOptions.yes;
  }

  if (propertyIdentifier == 'CreateDatabaseRow' &&
      parameterIdentifier == 'DatabaseId') {
    return AutomationParameterNeedOptions.yes;
  }

  if (propertyIdentifier == 'ArchiveDatabase' &&
      parameterIdentifier == 'DatabaseId') {
    return AutomationParameterNeedOptions.yes;
  }

  if (propertyIdentifier == 'ArchivePage' && parameterIdentifier == 'PageId') {
    return AutomationParameterNeedOptions.yes;
  }

  return AutomationParameterNeedOptions.no;
}

List<AutomationRadioModel> getOptionsFromFacts(
    Map<String, AutomationSchemaTriggerActionProperty> facts,
    AutomationSchemaTriggerActionProperty property) {
  return facts.entries
      .where((fact) =>
          fact.value.type.toLowerCase() == property.type.toLowerCase())
      .map((fact) => AutomationRadioModel(
            title: fact.value.name,
            description: fact.value.description,
            value: fact.key,
          ))
      .toList();
}
