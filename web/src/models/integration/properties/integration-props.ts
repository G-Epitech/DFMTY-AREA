import {
  IntegrationDiscordProps,
  IntegrationNotionProps,
  IntegrationOpenaiProps,
} from '@models/integration';

export type IntegrationProps =
  | IntegrationDiscordProps
  | IntegrationNotionProps
  | IntegrationOpenaiProps;
