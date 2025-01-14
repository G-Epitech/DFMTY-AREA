import {
  ChangeDetectionStrategy,
  Component,
  computed,
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
import { IntegrationsMediator } from '@mediators/integrations/integrations.mediator';
import { SchemaStore } from '@app/store/schema-store';
import {
  IntegrationAvailableProps,
  LinkFunction,
} from '@features/integrations/components/integration-add-dialog/integration-add-dialog.types';
import { NotionMediator } from '@mediators/integrations';

@Component({
  selector: 'tr-integration-add-dialog',
  imports: [
    TrDialogImports,
    BrnDialogImports,
    TrButtonDirective,
    IntegrationAvailableCardComponent,
    TrInputDirective,
    NgOptimizedImage,
    NgStyle,
  ],
  templateUrl: './integration-add-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAddDialogComponent {
  readonly #integrationsMediator = inject(IntegrationsMediator);
  readonly #notionMediator = inject(NotionMediator);
  readonly #schemaStore = inject(SchemaStore);

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
    notion: () => {
      this.#notionMediator.getUri().subscribe({
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

  #searchTerm = signal('');

  selectedIntegration = signal<IntegrationAvailableProps | null>(null);
  availableIntegrations = signal<IntegrationAvailableProps[]>([]);

  linkFn = computed(() => {
    const selected = this.selectedIntegration();
    return selected ? (this.#linkFunctions[selected.identifier] ?? null) : null;
  });

  filteredIntegrations = computed(() => {
    const searchTerm = this.#searchTerm().toLowerCase();
    const integrations = this.availableIntegrations();

    if (!searchTerm) return integrations;

    return integrations.filter(
      integration =>
        integration.name.toLowerCase().includes(searchTerm) ||
        integration.triggers.some(trigger =>
          trigger.toLowerCase().includes(searchTerm)
        ) ||
        integration.actions.some(action =>
          action.toLowerCase().includes(searchTerm)
        )
    );
  });

  constructor() {
    effect(() => {
      const schema = this.#schemaStore.getSchema();
      if (!schema) {
        return;
      }

      const integrations = Object.entries(schema.automationServices).map(
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
      );

      this.availableIntegrations.set(integrations);
    });
  }

  onSearch(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.#searchTerm.set(input.value);
  }

  selectIntegration(integrationProps: IntegrationAvailableProps): void {
    this.selectedIntegration.set(integrationProps);
  }

  backToAvailableIntegrations(): void {
    this.selectedIntegration.set(null);
  }

  linkIntegration(): void {
    const fn = this.linkFn();
    if (fn) fn();
  }
}
