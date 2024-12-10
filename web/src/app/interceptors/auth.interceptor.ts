import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { inject } from '@angular/core';
import { TokenMediator } from '@mediators/token.mediator';

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const tokenMediator = inject(TokenMediator);

  if (req.url.includes('auth')) {
    return next(req);
  }
  const accessToken = tokenMediator.getAccessToken();
  const request = attachAuthHeaders(req, accessToken);
  return next(request).pipe(
    catchError(error => {
      if (isUnauthorizedError(error)) {
        console.error('Unauthorized request', error);
        // TODO: Add handler for unauthorized requests (refresh token, etc.)
      }
      return throwError(() => error);
    })
  );
};

function attachAuthHeaders(
  req: HttpRequest<unknown>,
  accessToken: string | null
): HttpRequest<unknown> {
  if (!accessToken) {
    return req;
  }
  return req.clone({
    setHeaders: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
}

function isUnauthorizedError(error: unknown): boolean {
  return error instanceof HttpErrorResponse && error.status === 401;
}
