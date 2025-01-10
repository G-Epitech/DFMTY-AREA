import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
} from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { AuthMediator } from '@mediators/auth.mediator';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'tr-google-auth-button',
  imports: [NgOptimizedImage, TrButtonDirective],
  templateUrl: './google-auth-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GoogleAuthButtonComponent implements OnDestroy {
  readonly #authMediator = inject(AuthMediator);
  readonly #destroyRef = new Subject<void>();

  googleAuth() {
    this.#authMediator
      .getGoogleConfiguration()
      .pipe(takeUntil(this.#destroyRef))
      .subscribe(googleConfiguration => {
        console.log(googleConfiguration);
      });
  }

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }
}
