import {
  ChangeDetectionStrategy,
  Component,
  input,
  OnInit,
  signal,
} from '@angular/core';
import {
  IntegrationLeagueOfLegendsProps,
  IntegrationModel,
} from '@models/integration';
import { NgOptimizedImage, NgStyle } from '@angular/common';

@Component({
  selector: 'tr-integration-linked-league-of-legends',
  imports: [NgOptimizedImage, NgStyle],
  templateUrl: './integration-linked-league-of-legends.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedLeagueOfLegendsComponent implements OnInit {
  integration = input.required<IntegrationModel>();
  iconUri = input.required<string | null>();
  color = input.required<string | null>();

  props = signal<IntegrationLeagueOfLegendsProps | null>(null);

  ngOnInit() {
    this.props.set(this.integration().props as IntegrationLeagueOfLegendsProps);
  }
}
