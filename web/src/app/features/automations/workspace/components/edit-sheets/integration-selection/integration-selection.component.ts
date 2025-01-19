import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
  output,
  signal,
} from '@angular/core';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { AvailableIntegrationType } from '@common/types';
import { SchemaStore } from '@app/store/schema-store';
import { AvailableIntegrationButtonComponent } from '@components/buttons/available-integration-button/available-integration-button.component';
import { AutomationSchemaModel } from '@models/automation';

@Component({
  selector: 'tr-integration-selection',
  imports: [TrInputSearchComponent, AvailableIntegrationButtonComponent],
  templateUrl: './integration-selection.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationSelectionComponent {
  readonly #schemaStore = inject(SchemaStore);

  #searchTerm = signal<string>('');
  integrationSelected = output<AvailableIntegrationType>();

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

  onSearch(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.#searchTerm.set(input.value);
  }

  selectIntegration(integration: AvailableIntegrationType): void {
    this.integrationSelected.emit(integration);
  }
}
