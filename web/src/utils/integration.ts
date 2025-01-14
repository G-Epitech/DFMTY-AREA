import { IntegrationTypeEnum } from '@models/integration';

export function getIntegrationFromIdentifier(
  identifier: string
): IntegrationTypeEnum {
  return identifier.split('.')[0] as IntegrationTypeEnum;
}
