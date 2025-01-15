import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  signal,
} from '@angular/core';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { AvailableIntegrationType } from '@common/types';
import { SchemaStore } from '@app/store/schema-store';

@Component({
  selector: 'tr-integration-selection',
  imports: [TrInputSearchComponent],
  templateUrl: './integration-selection.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationSelectionComponent {
  readonly #schemaStore = inject(SchemaStore);

  availableIntegrations = signal<AvailableIntegrationType[]>([]);

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
}
