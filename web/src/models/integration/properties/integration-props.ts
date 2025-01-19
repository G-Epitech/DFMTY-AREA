import {
  IntegrationDiscordProps,
  IntegrationNotionProps,
  IntegrationOpenaiProps,
} from '@models/integration';
import { IntegrationLeagueOfLegendsProps } from '@models/integration/properties/integration-league-of-legends-props';

export type IntegrationProps =
  | IntegrationDiscordProps
  | IntegrationNotionProps
  | IntegrationOpenaiProps
  | IntegrationLeagueOfLegendsProps;
