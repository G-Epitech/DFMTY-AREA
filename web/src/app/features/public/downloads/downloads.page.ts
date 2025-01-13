import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-downloads',
  imports: [NgIcon, NgOptimizedImage, TrButtonDirective],
  templateUrl: './downloads.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class DownloadsPageComponent {}
