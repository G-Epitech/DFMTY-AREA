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
import { LinkFunction } from '@features/integrations/components/integration-add-dialog/integration-add-dialog.types';
import { GithubMediator, NotionMediator } from '@mediators/integrations';
import { IntegrationTypeEnum } from '@models/integration';
import { OpenaiLinkFormComponent } from '@features/integrations/openai/openai-link-form/openai-link-form.component';
import { AvailableIntegrationType } from '@common/types';
import { AutomationSchemaModel } from '@models/automation';
import { LeagueOfLegendsLinkFormComponent } from '@features/integrations/league-of-legends/league-of-legends-link-form/league-of-legends-link-form.component';

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
    OpenaiLinkFormComponent,
    LeagueOfLegendsLinkFormComponent,
  ],
  templateUrl: './integration-add-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAddDialogComponent {
  readonly #integrationsMediator = inject(IntegrationsMediator);
  readonly #notionMediator = inject(NotionMediator);
  readonly #githubMediator = inject(GithubMediator);
  readonly #schemaStore = inject(SchemaStore);

  readonly #linkFunctions: Record<string, LinkFunction> = {
    Discord: () => {
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
    Notion: () => {
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
    Github: () => {
      this.#githubMediator.getUri().subscribe({
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

  selectedIntegration = signal<AvailableIntegrationType | null>(null);
  availableIntegrations = signal<AvailableIntegrationType[]>([]);

  linkFn = computed(() => {
    const selected = this.selectedIntegration();
    return selected ? (this.#linkFunctions[selected.identifier] ?? null) : null;
  });

  filteredIntegrations = computed(() => {
    return AutomationSchemaModel.searchAvailableIntegrations(
      this.availableIntegrations(),
      this.#searchTerm()
    );
  });

  constructor() {
    effect(() => {
      const availableIntegrations =
        this.#schemaStore.getAvailableIntegrations();
      if (!availableIntegrations) {
        return;
      }
      this.availableIntegrations.set(availableIntegrations);
    });
  }

  onSearch(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.#searchTerm.set(input.value);
  }

  selectIntegration(integrationProps: AvailableIntegrationType): void {
    this.selectedIntegration.set(integrationProps);
  }

  backToAvailableIntegrations(): void {
    this.selectedIntegration.set(null);
  }

  linkIntegration(): void {
    const fn = this.linkFn();
    if (fn) fn();
  }

  protected readonly IntegrationTypeEnum = IntegrationTypeEnum;
}
