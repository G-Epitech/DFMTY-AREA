import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/models/automation.model.dart';

AutomationParameterType getParameterType(
    Automation automation,
    AutomationTriggerOrActionType type,
    String integrationName,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier) {
  switch (type) {
    case AutomationTriggerOrActionType.trigger:
      return _handleTriggerType(
          automation, integrationName, propertyIdentifier, parameterIdentifier);
    case AutomationTriggerOrActionType.action:
      return _handleActionType(automation, integrationName,
          indexOfTheTriggerOrAction, propertyIdentifier, parameterIdentifier);
  }
}

AutomationParameterType _handleTriggerType(
    Automation automation,
    String integrationName,
    String propertyIdentifier,
    String parameterIdentifier) {
  if (integrationName == IntegrationNames.discord) {
    return _handleDiscordTriggerType(
        automation, propertyIdentifier, parameterIdentifier);
  }

  if (integrationName == IntegrationNames.notion) {
    return _handleNotionTriggerType(propertyIdentifier, parameterIdentifier);
  }

  if (integrationName == IntegrationNames.leagueOfLegends &&
      parameterIdentifier == "KdaThreshold") {
    return AutomationParameterType.number;
  }

  return AutomationParameterType.choice;
}

AutomationParameterType _handleDiscordTriggerType(Automation automation,
    String propertyIdentifier, String parameterIdentifier) {
  if (propertyIdentifier == 'MessageReceivedInChannel') {
    if (parameterIdentifier == 'GuildId') {
      return AutomationParameterType.restrictedRadio;
    }
    if (parameterIdentifier == 'ChannelId') {
      return automation.trigger.parameters.isEmpty
          ? AutomationParameterType.restrictedRadioBlocked
          : AutomationParameterType.restrictedRadio;
    }
  }
  return AutomationParameterType.choice;
}

AutomationParameterType _handleNotionTriggerType(
    String propertyIdentifier, String parameterIdentifier) {
  if ((propertyIdentifier == 'DatabaseRowCreated' ||
          propertyIdentifier == 'DatabaseRowDeleted') &&
      parameterIdentifier == 'DatabaseId') {
    return AutomationParameterType.restrictedRadio;
  }
  return AutomationParameterType.choice;
}

AutomationParameterType _handleActionType(
    Automation automation,
    String integrationName,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier) {
  if (integrationName == IntegrationNames.discord) {
    return _handleDiscordActionType(automation, indexOfTheTriggerOrAction,
        propertyIdentifier, parameterIdentifier);
  }

  if (integrationName == IntegrationNames.notion) {
    return _handleNotionActionType(propertyIdentifier, parameterIdentifier);
  }

  return AutomationParameterType.choice;
}

AutomationParameterType _handleDiscordActionType(
    Automation automation,
    int indexOfTheTriggerOrAction,
    String propertyIdentifier,
    String parameterIdentifier) {
  if (propertyIdentifier == 'SendMessageToChannel') {
    if (parameterIdentifier == 'GuildId') {
      return AutomationParameterType.restrictedRadio;
    }
    if (parameterIdentifier == 'ChannelId') {
      for (final parameter
          in automation.actions[indexOfTheTriggerOrAction].parameters) {
        if (parameter.identifier == 'GuildId' && parameter.value.isNotEmpty) {
          return AutomationParameterType.restrictedRadio;
        }
      }
      return AutomationParameterType.restrictedRadioBlocked;
    }
  }
  return AutomationParameterType.choice;
}

AutomationParameterType _handleNotionActionType(
    String propertyIdentifier, String parameterIdentifier) {
  if (parameterIdentifier == "Icon") {
    return AutomationParameterType.emoji;
  }

  if ((propertyIdentifier == 'CreateDatabase' ||
          propertyIdentifier == 'CreatePage') &&
      parameterIdentifier == 'ParentId') {
    return AutomationParameterType.restrictedRadio;
  }

  if (propertyIdentifier == 'CreateDatabaseRow' &&
      parameterIdentifier == 'DatabaseId') {
    return AutomationParameterType.restrictedRadio;
  }

  if (propertyIdentifier == 'ArchiveDatabase' &&
      parameterIdentifier == 'DatabaseId') {
    return AutomationParameterType.restrictedRadio;
  }

  if (propertyIdentifier == 'ArchivePage' && parameterIdentifier == 'PageId') {
    return AutomationParameterType.restrictedRadio;
  }

  return AutomationParameterType.choice;
}

List<AutomationRadioModel> getTypeFromFacts(
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
