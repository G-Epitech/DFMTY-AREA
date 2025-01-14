import {
  IntegrationDiscordProps,
  IntegrationNotionProps,
} from '@models/integration';

export type IntegrationProps =
  | IntegrationDiscordProps
  | IntegrationNotionProps
  | string;
