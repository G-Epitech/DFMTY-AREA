import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { TrDialogImports } from '@triggo-ui/dialog';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  IntegrationAvailableCardComponent,
  IntegrationAvailableCardProps,
} from '@features/integrations/components/integration-available-card/integration-available-card.component';
import { TrInputDirective } from '@triggo-ui/input';
import { NgOptimizedImage } from '@angular/common';
import { TrIconComponent } from '@triggo-ui/icon';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-integration-add-dialog',
  imports: [
    TrDialogImports,
    BrnDialogImports,
    TrButtonDirective,
    IntegrationAvailableCardComponent,
    TrInputDirective,
    NgOptimizedImage,
    TrIconComponent,
    NgIcon,
  ],
  templateUrl: './integration-add-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAddDialogComponent {
  selectedIntegration = signal<IntegrationAvailableCardProps | null>(null);

  readonly availableIntegrations: IntegrationAvailableCardProps[] = [
    {
      logoAssetName: 'icons/discord_logo.svg',
      name: 'Discord',
      description:
        'Connect your Discord server to Triggo and receive notifications about your projects.',
      features: [
        'Receive notifications about your projects',
        'Customize your notifications',
      ],
    },
  ];

  selectIntegration(integration: IntegrationAvailableCardProps): void {
    this.selectedIntegration.set(integration);
  }

  backToAvailableIntegrations(): void {
    this.selectedIntegration.set(null);
  }
}
