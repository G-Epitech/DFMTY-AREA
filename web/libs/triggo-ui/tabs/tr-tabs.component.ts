import { Component, input, ChangeDetectionStrategy } from '@angular/core';
import { BrnTabsDirective } from '@spartan-ng/ui-tabs-brain';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'tr-tabs',
  standalone: true,
  hostDirectives: [
    {
      directive: BrnTabsDirective,
      inputs: ['orientation', 'direction', 'activationMode', 'brnTabs: tab'],
      outputs: ['tabActivated'],
    },
  ],
  template: '<ng-content/>',
})
export class TrTabsComponent {
  public readonly tab = input.required<string>();
}
