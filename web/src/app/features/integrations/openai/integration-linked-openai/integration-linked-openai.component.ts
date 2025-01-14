import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  signal,
} from '@angular/core';
import { IntegrationModel, IntegrationOpenaiProps } from '@models/integration';
import { NgOptimizedImage, NgStyle } from '@angular/common';

@Component({
  selector: 'tr-integration-linked-openai',
  imports: [NgOptimizedImage, NgStyle],
  templateUrl: './integration-linked-openai.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedOpenaiComponent {
  integration = input.required<IntegrationModel>();
  iconUri = input.required<string | null>();
  color = input.required<string | null>();

  openaiProps = signal<IntegrationOpenaiProps | null>(null);

  constructor() {
    effect(() => {
      if (this.integration()) {
        this.openaiProps.set(
          this.integration().props as IntegrationOpenaiProps
        );
      }
    });
  }
}
