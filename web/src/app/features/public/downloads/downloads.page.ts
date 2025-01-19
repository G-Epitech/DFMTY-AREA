import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { AppRouter } from '@app/app.router';

@Component({
  selector: 'tr-downloads',
  imports: [NgOptimizedImage, TrButtonDirective],
  templateUrl: './downloads.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class DownloadsPageComponent {
  readonly #appRouter = inject(AppRouter);

  navigateToAPK() {
    this.#appRouter.redirectToDownloadAPK();
  }
}
