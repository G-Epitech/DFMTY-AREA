import {
  ChangeDetectionStrategy,
  Component,
} from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { TrInputSearchComponent } from '@triggo-ui/input';

@Component({
  selector: 'tr-integration-selection',
  imports: [AsyncPipe, TrInputSearchComponent],
  templateUrl: './integration-selection.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationSelectionComponent {}
