import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { TrDialogImports } from '@triggo-ui/dialog';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrInputSearchComponent } from '@triggo-ui/input';

interface GuildProps {
  id: string;
  name: string;
  iconUri: string;
  approximateMemberCount: number;
  linked: boolean;
}

@Component({
  selector: 'tr-manage-guild-dialog',
  imports: [
    NgOptimizedImage,
    TrDialogImports,
    BrnDialogImports,
    NgIcon,
    NgClass,
    TrButtonDirective,
    TrInputSearchComponent,
  ],
  templateUrl: './manage-guild-dialog.component.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ManageGuildDialogComponent {
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
    {
      id: '5',
      name: 'Mario Kart Fans',
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 100,
      linked: false,
    },
    {
      id: '6',
      name: 'Mario Kart Fans',
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 100,
      linked: false,
    },
    {
      id: '7',
      name: 'Mario Kart Fans',
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 100,
      linked: false,
    },
    {
      id: '8',
      name: 'Mario Kart Fans',
      iconUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      approximateMemberCount: 100,
      linked: false,
    },
  ];
}
