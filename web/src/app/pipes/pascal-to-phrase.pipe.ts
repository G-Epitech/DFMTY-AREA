import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  standalone: true,
  name: 'pascalToPhrase',
})
export class PascalToPhrasePipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return '';

    let result = value.charAt(0).toUpperCase();

    for (let i = 1; i < value.length; i++) {
      const currentChar = value.charAt(i);

      if (currentChar === currentChar.toUpperCase()) {
        result += ' ' + currentChar;
      } else {
        result += currentChar;
      }
    }

    return result;
  }
}
