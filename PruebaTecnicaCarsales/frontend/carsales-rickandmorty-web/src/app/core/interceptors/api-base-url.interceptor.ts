import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export const apiBaseUrlInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.url.startsWith('/api')) {
    const newReq = req.clone({
      url: `${environment.apiBaseUrl}${req.url}`
    });

    return next(newReq);
  }

  return next(req);
};