import { IntegrationProps, IntegrationTypeEnum } from '@models/integration';

export interface IntegrationDTO {
  id: string;
  ownerId: string;
  isValid: boolean;
  type: IntegrationTypeEnum;
  properties: IntegrationProps;
}
