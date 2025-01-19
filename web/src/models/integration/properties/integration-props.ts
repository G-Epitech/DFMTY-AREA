import {
  IntegrationDiscordProps,
  IntegrationGmailProps,
  IntegrationNotionProps,
  IntegrationOpenaiProps,
} from '@models/integration';
import { IntegrationLeagueOfLegendsProps } from '@models/integration/properties/integration-league-of-legends-props';
import { IntegrationGithubProps } from '@models/integration/properties/integration-github-props';

export type IntegrationProps =
  | IntegrationDiscordProps
  | IntegrationNotionProps
  | IntegrationOpenaiProps
  | IntegrationLeagueOfLegendsProps
  | IntegrationGithubProps
  | IntegrationGmailProps;
