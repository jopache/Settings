import { Injectable, Injector } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';

import { AuthenticationService } from './authentication.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ApplicationInterceptor implements HttpInterceptor {
  constructor(private inj: Injector) {}

  getAuthService(): AuthenticationService {
    return this.inj.get(AuthenticationService);
  }

  // constructor() {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authService = this.getAuthService();

    let headers = new HttpHeaders().set('Content-Type', 'application/json');

    if (authService.isAuthenticated()) {
        const value = 'Bearer ' + authService.getToken();
        headers = headers.append('Authorization', value);
    }
    request = request.clone({
      headers: headers
    });
    return next.handle(request);
  }
}
