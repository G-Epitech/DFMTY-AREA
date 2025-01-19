import { IntegrationTypeEnum } from '@models/integration';

export function getIntegrationFromIdentifier(
  identifier: string
): IntegrationTypeEnum {
  return identifier.split('.')[0] as IntegrationTypeEnum;
}

export function integrationTypeFromIdentifier(
  identifier: string
): IntegrationTypeEnum {
  switch (identifier) {
    case 'Notion':
      return IntegrationTypeEnum.NOTION;
    case 'Discord':
      return IntegrationTypeEnum.DISCORD;
    case 'OpenAi':
      return IntegrationTypeEnum.OPENAI;
    case 'LeagueOfLegends':
      return IntegrationTypeEnum.LEAGUE_OF_LEGENDS;
    case 'Github':
      return IntegrationTypeEnum.GITHUB;
    default:
      throw new Error(`Unsupported integration type: ${identifier}`);
  }
}
