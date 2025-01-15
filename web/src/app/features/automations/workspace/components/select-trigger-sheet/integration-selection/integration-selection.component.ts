import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
  signal,
} from '@angular/core';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { AvailableIntegrationType } from '@common/types';
import { SchemaStore } from '@app/store/schema-store';
import { AvailableIntegrationListCardComponent } from '@components/available-integration-list-card/available-integration-list-card.component';
import { IntegrationTypeEnum } from '@models/integration';
import { AutomationSchemaModel } from '@models/automation';

@Component({
  selector: 'tr-integration-selection',
  imports: [TrInputSearchComponent, AvailableIntegrationListCardComponent],
  templateUrl: './integration-selection.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationSelectionComponent {
  readonly #schemaStore = inject(SchemaStore);

  #searchTerm = signal<string>('');

  availableIntegrations = signal<AvailableIntegrationType[]>([]);
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

  forceWhite(integration: AvailableIntegrationType): boolean {
    return integration.identifier == IntegrationTypeEnum.OPENAI;
  }

  onSearch(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.#searchTerm.set(input.value);
  }
}
