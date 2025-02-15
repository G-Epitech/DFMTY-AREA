import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  output,
  signal,
} from '@angular/core';
import {
  IntegrationDiscordProps,
  IntegrationGmailProps,
  IntegrationLeagueOfLegendsProps,
  IntegrationModel,
  IntegrationNotionProps,
  IntegrationOpenaiProps,
  IntegrationTypeEnum,
} from '@models/integration';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';
import { IntegrationGithubProps } from '@models/integration/properties/integration-github-props';

@Component({
  selector: 'tr-linked-integration-button',
  imports: [TrButtonDirective, NgIcon],
  templateUrl: './linked-integration-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LinkedIntegrationButtonComponent {
  integration = input.required<IntegrationModel>();
  displayIcon = input<boolean>(false);

  header = signal<string>('');
  subheader = signal<string>('');

  selectLinkedIntegration = output<IntegrationModel>();

  constructor() {
    effect(() => {
      if (this.integration()) {
        this._assignProps();
      }
    });
  }

  _assignProps() {
    switch (this.integration().type) {
      case IntegrationTypeEnum.OPENAI: {
        const openaiProps = this.integration().props as IntegrationOpenaiProps;
        this.header.set(openaiProps.ownerName);
        this.subheader.set(openaiProps.ownerEmail);
        break;
      }
      case IntegrationTypeEnum.DISCORD: {
        const discordProps = this.integration()
          .props as IntegrationDiscordProps;
        this.header.set(discordProps.displayName);
        this.subheader.set(discordProps.email);
        break;
      }
      case IntegrationTypeEnum.NOTION: {
        const notionProps = this.integration().props as IntegrationNotionProps;
        this.header.set(notionProps.name + ' - ' + notionProps.workspaceName);
        this.subheader.set(notionProps.email);
        break;
      }
      case IntegrationTypeEnum.LEAGUE_OF_LEGENDS: {
        const leagueOfLegendsProps = this.integration()
          .props as IntegrationLeagueOfLegendsProps;
        this.header.set(leagueOfLegendsProps.riotGameName);
        this.subheader.set(leagueOfLegendsProps.riotTagLine);
        break;
      }
      case IntegrationTypeEnum.GITHUB: {
        const githubProps = this.integration().props as IntegrationGithubProps;
        this.header.set(githubProps.name);
        this.subheader.set(githubProps.email || githubProps.bio || '');
        break;
      }
      case IntegrationTypeEnum.GMAIL: {
        const gmailProps = this.integration().props as IntegrationGmailProps;
        this.header.set(gmailProps.displayName + ' - ' + gmailProps.familyName);
        this.subheader.set(gmailProps.email || '');
        break;
      }
    }
  }

  selectIntegration() {
    this.selectLinkedIntegration.emit(this.integration());
  }
}
