import { IntegrationTypeEnum } from '@models/integration/integration-type.enum';
import { IntegrationDiscordProps } from '@models/integration/properties';

export class IntegrationModel {
  readonly id: string;
  readonly ownerId: string;
  readonly isValid: boolean;
  readonly type: IntegrationTypeEnum;
  readonly props: IntegrationDiscordProps | string;

  constructor(
    id: string,
    ownerId: string,
    isValid: boolean,
    type: IntegrationTypeEnum,
    props: IntegrationDiscordProps | string
  ) {
    this.id = id;
    this.ownerId = ownerId;
    this.isValid = isValid;
    this.type = type;
    this.props = props;
  }
}
