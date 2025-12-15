import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const apiErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  return next(req).pipe(
    catchError((error) => {
      if (error instanceof HttpErrorResponse) {

        if (error.status === 404) {
          router.navigateByUrl('/not-found');
        } else if (error.status >= 500) {
          router.navigateByUrl('/error');
        }
      }

      return throwError(() => error);
    })
  );
};