import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LabelDirective } from '@triggo-ui/label';
import { TrInputDirective } from '@triggo-ui/input';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-openai-link-form',
  imports: [FormsModule, LabelDirective, TrInputDirective, TrButtonDirective],
  templateUrl: './openai-link-form.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class OpenaiLinkFormComponent {}
