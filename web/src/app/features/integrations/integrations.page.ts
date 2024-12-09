import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  IntegrationModel,
  IntegrationTypeEnum,
  IntegrationDiscordProps,
} from '@models/integration';
import { Observable, of } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { IntegrationLinkedCardComponent } from '@features/integrations/components/integration-linked/integration-linked-card.component';
import { IntegrationAddDialogComponent } from '@features/integrations/components/integration-add-dialog/integration-add-dialog.component';
import { PaginationComponent } from '@app/components';
import { TrInputDirective } from '@triggo-ui/input';

@Component({
  selector: 'tr-integrations',
  imports: [
    AsyncPipe,
    IntegrationLinkedCardComponent,
    IntegrationAddDialogComponent,
    PaginationComponent,
    TrInputDirective,
  ],
  templateUrl: './integrations.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationsPageComponent {
  readonly integrations: Observable<IntegrationModel[]>;

  constructor() {
    const discordProps1: IntegrationDiscordProps = {
      id: '12345',
      email: 'user1@example.com',
      username: 'user1',
      displayName: 'User One',
      avatarUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      flags: ['flag1', 'flag2'],
    };

    const discordProps2: IntegrationDiscordProps = {
      id: '67890',
      email: 'user2@example.com',
      username: 'user2',
      displayName: 'User Two',
      avatarUri:
        'https://play.nintendo.com/images/profile-mk-kamek.7bf2a8f2.aead314d58b63e27.png',
      flags: ['flag3'],
    };

    this.integrations = of([
      new IntegrationModel(
        '1',
        'owner1',
        true,
        IntegrationTypeEnum.DISCORD,
        discordProps1
      ),
      new IntegrationModel(
        '2',
        'owner2',
        false,
        IntegrationTypeEnum.DISCORD,
        discordProps2
      ),
    ]);
  }
}
