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
    case 'notion':
      return IntegrationTypeEnum.NOTION;
    case 'discord':
      return IntegrationTypeEnum.DISCORD;
    case 'openAi':
      return IntegrationTypeEnum.OPENAI;
    default:
      throw new Error(`Unsupported integration type: ${identifier}`);
  }
}
