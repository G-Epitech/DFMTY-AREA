import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  signal,
} from '@angular/core';
import { TrDialogImports } from '@triggo-ui/dialog';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { TrButtonDirective } from '@triggo-ui/button';
import { IntegrationAvailableCardComponent } from '@features/integrations/components/integration-available-card/integration-available-card.component';
import { TrInputDirective } from '@triggo-ui/input';
import { NgOptimizedImage, NgStyle } from '@angular/common';
import { NgIcon } from '@ng-icons/core';
import { IntegrationsMediator } from '@mediators/integrations.mediator';
import { SchemaStore } from '@app/store/schema-store';

interface IntegrationAvailableProps {
  name: string;
  iconUri: string;
  identifier: string;
  triggers: string[];
  actions: string[];
  color: string;
}

type LinkFunction = () => void;

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
    NgStyle,
  ],
  templateUrl: './integration-add-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAddDialogComponent {
  readonly #integrationsMediator = inject(IntegrationsMediator);
  readonly #schemaStore = inject(SchemaStore);

  selectedIntegration = signal<IntegrationAvailableProps | null>(null);
  availableIntegrations = signal<IntegrationAvailableProps[]>([]);
  linkFn = signal<LinkFunction | null>(null);

  readonly #linkFunctions: Record<string, LinkFunction> = {
    discord: () => {
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
  };

  constructor() {
    effect(() => {
      const schema = this.#schemaStore.getSchema();
      if (!schema) {
        return;
      }
      this.availableIntegrations.set(
        Object.entries(schema.automationServices).map(
          ([name, integration]) => ({
            color: integration.color,
            name: integration.name,
            iconUri: integration.iconUri,
            identifier: name,
            triggers: Object.entries(integration.triggers).map(
              ([, trigger]) => trigger.name
            ),
            actions: Object.entries(integration.actions).map(
              ([, action]) => action.name
            ),
          })
        )
      );
    });
    effect(() => {
      const selectedIntegration = this.selectedIntegration();
      if (!selectedIntegration) {
        return;
      }
      this.linkFn.set(this.#linkFunctions[selectedIntegration.identifier]);
    });
  }

  selectIntegration(integrationProps: IntegrationAvailableProps): void {
    this.selectedIntegration.set(integrationProps);
  }

  backToAvailableIntegrations(): void {
    this.selectedIntegration.set(null);
  }

  linkIntegration(): void {
    const linkFn = this.linkFn();
    if (linkFn) {
      linkFn();
    }
  }
}
