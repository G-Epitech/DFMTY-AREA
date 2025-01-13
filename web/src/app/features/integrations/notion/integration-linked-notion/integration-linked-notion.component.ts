import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  signal,
} from '@angular/core';
import { IntegrationDiscordProps, IntegrationModel } from '@models/integration';

@Component({
  selector: 'tr-integration-linked-notion',
  imports: [],
  templateUrl: './integration-linked-notion.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedNotionComponent {
  integration = input.required<IntegrationModel>();
  iconUri = input.required<string>();
  color = input.required<string>();
}
