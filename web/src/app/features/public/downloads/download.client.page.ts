import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { AppRouter } from '@app/app.router';

@Component({
  selector: 'tr-client',
  templateUrl: './download.client.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [NgOptimizedImage],
})
export class DownloadClientComponent implements OnInit {
  readonly #appRouter = inject(AppRouter);

  ngOnInit() {
    const link = document.createElement('a');
    link.href = 'Triggo-release.apk';
    link.download = 'Triggo-release.apk';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

  navigateToFAQ() {
    this.#appRouter.redirectToFAQ();
  }

  navigateToDocs() {
    this.#appRouter.redirectToDocs();
  }
}
