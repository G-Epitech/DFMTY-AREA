import { IntegrationTypeEnum } from '@models/integration/integration-type.enum';
import {
  IntegrationDiscordProps,
  IntegrationProps,
} from '@models/integration/properties';

export class IntegrationModel {
  readonly id: string;
  readonly ownerId: string;
  readonly isValid: boolean;
  readonly type: IntegrationTypeEnum;
  readonly #discordProps: IntegrationDiscordProps | undefined;
  readonly #gmailProps: string | undefined;

  constructor(
    id: string,
    ownerId: string,
    isValid: boolean,
    type: IntegrationTypeEnum,
    properties: IntegrationDiscordProps | string
  ) {
    console.log(properties);
    this.id = id;
    this.ownerId = ownerId;
    this.isValid = isValid;
    this.type = type;
    console.log(type);
    switch (type) {
      case IntegrationTypeEnum.DISCORD:
        this.#discordProps = properties as IntegrationDiscordProps;
        console.log(properties);
        break;
      case IntegrationTypeEnum.GMAIL:
        this.#gmailProps = properties as string;
        break;
    }
  }

  get discordProps(): IntegrationDiscordProps {
    return this.#discordProps!;
  }

  get gmailProps(): string {
    return this.#gmailProps!;
  }
}
