import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AppRouter extends Router {
  redirectToLogin() {
    void this.navigate(['/login']);
  }

  redirectToHome() {
    void this.navigate(['/home']);
  }
}
