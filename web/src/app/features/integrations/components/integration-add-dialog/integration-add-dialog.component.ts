import {
  ChangeDetectionStrategy,
  Component,
  inject,
  signal,
} from '@angular/core';
import { TrDialogImports } from '@triggo-ui/dialog';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  IntegrationAvailableCardComponent,
  IntegrationAvailableCardProps,
} from '@features/integrations/components/integration-available-card/integration-available-card.component';
import { TrInputDirective } from '@triggo-ui/input';
import { NgOptimizedImage } from '@angular/common';
import { NgIcon } from '@ng-icons/core';
import { IntegrationsMediator } from '@mediators/integrations.mediator';

@Component({
  selector: 'tr-integration-add-dialog',
  imports: [
    TrDialogImports,
    BrnDialogImports,
    TrButtonDirective,
    IntegrationAvailableCardComponent,
    TrInputDirective,
    NgOptimizedImage,
    NgIcon,
  ],
  templateUrl: './integration-add-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAddDialogComponent {
  selectedIntegration = signal<IntegrationAvailableCardProps | null>(null);
  readonly #integrationsMediator = inject(IntegrationsMediator);

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
      linkFn: () => {
        this.#integrationsMediator.discordRepository.getUri().subscribe({
          next: uri => {
            if (!uri) {
              return;
            }
            const newWindow = window.open(`${uri}`, '_blank');
            if (newWindow) {
              newWindow.opener = window;
            }
          },
        });
      },
    },
  ];

  selectIntegration(integration: IntegrationAvailableCardProps): void {
    this.selectedIntegration.set(integration);
  }

  backToAvailableIntegrations(): void {
    this.selectedIntegration.set(null);
  }
}
