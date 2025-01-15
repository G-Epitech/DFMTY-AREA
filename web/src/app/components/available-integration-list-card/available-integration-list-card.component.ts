import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgOptimizedImage, NgStyle } from '@angular/common';
import { IntegrationTypeEnum } from '@models/integration';
import { AvailableIntegrationType } from '@common/types';

@Component({
  selector: 'tr-available-integration-list-card',
  imports: [TrButtonDirective, NgOptimizedImage, NgStyle],
  templateUrl: './available-integration-list-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AvailableIntegrationListCardComponent {
  integration = input.required<AvailableIntegrationType>();

  forceWhite(): boolean {
    return this.integration().identifier === IntegrationTypeEnum.OPENAI;
  }
}
