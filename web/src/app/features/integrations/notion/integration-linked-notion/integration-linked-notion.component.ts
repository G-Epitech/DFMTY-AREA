import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  signal,
} from '@angular/core';
import { IntegrationModel, IntegrationNotionProps } from '@models/integration';
import { NgOptimizedImage, NgStyle } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-integration-linked-notion',
  imports: [NgOptimizedImage, TrButtonDirective, NgStyle],
  templateUrl: './integration-linked-notion.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedNotionComponent {
  integration = input.required<IntegrationModel>();
  iconUri = input.required<string>();
  color = input.required<string>();

  notionProps = signal<IntegrationNotionProps | null>(null);

  constructor() {
    effect(() => {
      if (this.integration()) {
        this.notionProps.set(
          this.integration().props as IntegrationNotionProps
        );
      }
    });
  }
}
