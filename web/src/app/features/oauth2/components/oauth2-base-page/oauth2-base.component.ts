import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  signal,
} from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrSpinnerComponent } from '@triggo-ui/spinner';

@Component({
  selector: 'tr-oauth2-base-page',
  imports: [NgIcon, NgOptimizedImage, TrButtonDirective, TrSpinnerComponent],
  templateUrl: './oauth2-base.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class Oauth2BaseComponent {
  name = input.required<string>();
  loading = input.required<boolean>();
  success = input.required<boolean>();

  countdown = signal<number>(5);

  constructor() {
    effect(() => {
      if (this.success()) {
        this.startCountdown();
      }
    });
  }

  startCountdown(): void {
    let count = 5;
    const interval = setInterval(() => {
      count -= 1;
      this.countdown.set(count);
      if (count === 0) {
        clearInterval(interval);
        this.handleCloseWindow();
      }
    }, 1000);
  }

  handleCloseWindow(): void {
    if (window.opener) {
      this.closeWindow();
    } else {
      window.location.href = '/';
    }
  }

  closeWindow(): void {
    window.close();
  }
}
