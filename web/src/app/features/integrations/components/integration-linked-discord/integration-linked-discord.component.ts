import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { IntegrationDiscordProps } from '@models/integration';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { TrDialogImports } from '@triggo-ui/dialog';
import { NgIcon } from '@ng-icons/core';

interface GuildProps {
  id: string;
  name: string;
  iconUri: string;
  approximateMemberCount: number;
  linked: boolean;
}

@Component({
  selector: 'tr-integration-linked-discord',
  imports: [
    NgOptimizedImage,
    TrButtonDirective,
    TrDialogImports,
    BrnDialogImports,
    NgIcon,
    NgClass,
  ],
  templateUrl: './integration-linked-discord.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedDiscordComponent {
  props = input.required<IntegrationDiscordProps>();

  readonly guilds: GuildProps[] = [
    {
      id: '1',
      name: "Programmer's Hangout",
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 500,
      linked: true,
    },
    {
      id: '2',
      name: 'Angular Devs',
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 21300,
      linked: false,
    },
    {
      id: '3',
      name: 'Triggo Lovers',
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 1232100,
      linked: false,
    },
    {
      id: '4',
      name: 'Mario Kart Fans',
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 100,
      linked: false,
    },
  ];
}
