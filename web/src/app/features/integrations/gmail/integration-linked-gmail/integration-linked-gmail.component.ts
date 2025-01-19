import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  signal,
} from '@angular/core';
import { IntegrationGmailProps, IntegrationModel } from '@models/integration';
import { NgOptimizedImage, NgStyle } from '@angular/common';

@Component({
  selector: 'tr-integration-linked-gmail',
  imports: [NgOptimizedImage],
  templateUrl: './integration-linked-gmail.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedGmailComponent {
  integration = input.required<IntegrationModel>();
  iconUri = input.required<string | null>();
  color = input.required<string | null>();

  props = signal<IntegrationGmailProps | null>(null);

  constructor() {
    effect(() => {
      if (this.integration()) {
        this.props.set(this.integration().props as IntegrationGmailProps);
      }
    });
  }
}
