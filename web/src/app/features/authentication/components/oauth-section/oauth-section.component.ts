import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  GoogleAuthButtonComponent
} from '@components/google-auth-button/google-auth-button.component';

@Component({
  selector: 'tr-oauth-section',
  imports: [NgOptimizedImage, TrButtonDirective, GoogleAuthButtonComponent],
  templateUrl: './oauth-section.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class OauthSectionComponent {}
