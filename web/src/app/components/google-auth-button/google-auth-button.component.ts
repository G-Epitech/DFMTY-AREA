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
import { GoogleMediator } from '@mediators/google.mediator';

@Component({
  selector: 'tr-google-auth-button',
  imports: [NgOptimizedImage, TrButtonDirective],
  templateUrl: './google-auth-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GoogleAuthButtonComponent implements OnDestroy {
  readonly #googleMediator = inject(GoogleMediator);
  readonly #destroyRef = new Subject<void>();

  googleAuth() {
    this.#googleMediator
      .getGoogleConfiguration()
      .pipe(takeUntil(this.#destroyRef))
      .subscribe(googleConfiguration => {
        const authUrl = googleConfiguration.constructAuthUrl();
        window.open(authUrl, '_self');
      });
  }

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }
}
