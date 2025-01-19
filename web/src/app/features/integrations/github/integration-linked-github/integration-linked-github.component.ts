import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  signal,
} from '@angular/core';
import { IntegrationModel } from '@models/integration';
import { IntegrationGithubProps } from '@models/integration/properties/integration-github-props';
import { NgOptimizedImage, NgStyle } from '@angular/common';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-integration-linked-github',
  imports: [NgOptimizedImage, NgStyle, NgIcon],
  templateUrl: './integration-linked-github.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedGithubComponent {
  integration = input.required<IntegrationModel>();
  iconUri = input.required<string | null>();
  color = input.required<string | null>();

  props = signal<IntegrationGithubProps | null>(null);

  constructor() {
    effect(() => {
      if (this.integration()) {
        this.props.set(this.integration().props as IntegrationGithubProps);
      }
    });
  }
}
