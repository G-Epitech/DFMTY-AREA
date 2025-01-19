import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { NgClass } from '@angular/common';

@Component({
  selector: 'tr-icon-box',
  imports: [NgIcon, NgClass],
  templateUrl: './icon-box.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IconBoxComponent {
  color = input.required<string>();
  icon = input.required<string>();
}
