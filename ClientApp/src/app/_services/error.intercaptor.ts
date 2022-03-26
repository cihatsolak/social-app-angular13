import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((httpErrorResponse: HttpErrorResponse) => {
        if (httpErrorResponse.status === 400) {
          return throwError(() => new Error(httpErrorResponse.error.message));
        }

        if (httpErrorResponse.status === 401) {
          return throwError(() => new Error(httpErrorResponse.statusText));
        }

        if (httpErrorResponse.status === 500) {
          return throwError(() => new Error(httpErrorResponse.error.Message));
        }

        return throwError(() => new Error(httpErrorResponse.statusText));
      })
    );
  }
}
