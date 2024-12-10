import { IntegrationTypeEnum } from '@models/integration/integration-type.enum';
import { IntegrationDiscordProps } from '@models/integration/properties';

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
    props: IntegrationDiscordProps | string
  ) {
    this.id = id;
    this.ownerId = ownerId;
    this.isValid = isValid;
    this.type = type;
    switch (type) {
      case IntegrationTypeEnum.DISCORD:
        this.#discordProps = props as IntegrationDiscordProps;
        break;
      case IntegrationTypeEnum.GMAIL:
        this.#gmailProps = props as string;
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
