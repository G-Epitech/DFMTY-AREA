import { IntegrationTypeEnum } from '@models/integration/integration-type.enum';
import { IntegrationProps } from '@models/integration/properties';

export class IntegrationModel {
  readonly id: string;
  readonly ownerId: string;
  readonly isValid: boolean;
  readonly type: IntegrationTypeEnum;
  readonly #props: IntegrationProps;

  constructor(
    id: string,
    ownerId: string,
    isValid: boolean,
    type: IntegrationTypeEnum,
    properties: IntegrationProps
  ) {
    this.id = id;
    this.ownerId = ownerId;
    this.isValid = isValid;
    this.type = type;
    this.#props = properties;
  }

  get props(): IntegrationProps {
    return this.#props;
  }
}
