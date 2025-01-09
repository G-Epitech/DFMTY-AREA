import { Pipe, PipeTransform } from '@angular/core';
import { ValidationErrors } from '@angular/forms';

@Pipe({
  name: 'validationErrors',
  standalone: true,
})
export class ValidationErrorsPipe implements PipeTransform {
  readonly #errorMessages: Record<string, string> = {
    required: 'This field is required',
    email: 'Invalid email',
    minlength: 'Password must be at least 8 characters long',
    passwordMismatch: 'Passwords do not match',
  };

  transform(errors: ValidationErrors | null | undefined): string {
    if (errors) {
      const errorKey = Object.keys(errors)[0];
      return this.#errorMessages[errorKey];
    }
    return '';
  }
}
