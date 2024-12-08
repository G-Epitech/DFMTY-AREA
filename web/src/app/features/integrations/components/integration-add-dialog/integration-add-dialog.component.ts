import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TrDialogImports } from '@triggo-ui/dialog';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  IntegrationAvailableCardComponent,
  IntegrationAvailableCardProps,
} from '@features/integrations/components/integration-available-card/integration-available-card.component';
import { TrInputDirective } from '@triggo-ui/input';

@Component({
  selector: 'tr-integration-add-dialog',
  imports: [
    TrDialogImports,
    BrnDialogImports,
    TrButtonDirective,
    IntegrationAvailableCardComponent,
    TrInputDirective,
  ],
  templateUrl: './integration-add-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAddDialogComponent {
  readonly availableIntegrations: IntegrationAvailableCardProps[] = [
    {
      logoAssetName: 'icons/discord_logo.svg',
      name: 'Discord',
      description:
        'Connect your Discord server to Triggo and receive notifications about your projects.',
    },
  ];
}
