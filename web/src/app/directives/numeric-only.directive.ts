import { Directive, HostListener } from '@angular/core';

@Directive({
  standalone: true,
  selector: '[trNumericOnly]',
})
export class NumericOnlyDirective {
  @HostListener('input', ['$event']) onInputChange(event: InputEvent): void {
    const input = event.target as HTMLInputElement;
    const sanitizedValue = input.value.replace(/[^0-9]/g, '');
    if (sanitizedValue !== input.value) {
      input.value = sanitizedValue;
      const event = new Event('input', { bubbles: true });
      input.dispatchEvent(event);
    }
  }
}
