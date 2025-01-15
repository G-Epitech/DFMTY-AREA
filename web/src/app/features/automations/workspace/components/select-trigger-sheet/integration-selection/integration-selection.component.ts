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
import {
  AvailableIntegrationListCardComponent
} from '@components/available-integration-list-card/available-integration-list-card.component';
import { IntegrationTypeEnum } from '@models/integration';

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

  forceWhite(integration: AvailableIntegrationType): boolean {
    return integration.identifier == IntegrationTypeEnum.OPENAI;
  }
}
