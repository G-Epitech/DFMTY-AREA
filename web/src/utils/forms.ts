import { AbstractControl } from '@angular/forms';

export function isControlInvalid(control: AbstractControl): boolean {
  return control.invalid && control.touched;
}
