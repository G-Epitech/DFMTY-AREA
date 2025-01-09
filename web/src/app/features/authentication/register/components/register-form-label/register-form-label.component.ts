import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { LabelDirective } from '@triggo-ui/label';
import { NgClass } from '@angular/common';
import { ValidationErrors } from '@angular/forms';
import { ValidationErrorsPipe } from '@features/authentication/register/pipes/validation-errors.pipe';

@Component({
  selector: 'tr-register-form-label',
  imports: [LabelDirective, NgClass, ValidationErrorsPipe],
  templateUrl: './register-form-label.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class RegisterFormLabelComponent {
  for = input.required<string>();
  name = input.required<string>();
  untouched = input<boolean>();
  invalid = input<boolean>();
  errors = input<ValidationErrors | null>();

  error = computed(() => !this.untouched() && this.invalid());
}
