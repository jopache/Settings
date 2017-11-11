import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthenticationService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private authUrl = environment.backendUrl + '/api/authentication/jwt';

  isAuthenticated = false;
  token: string;

  constructor(private http: Http) { }

  authenticate(username: string, password: string): Promise<boolean> {

    const promise = this.http.post(this.authUrl, {
      userName: username,
      password: password
    })
    .toPromise()
    .then(response => {
      this.token = response.json().token;
      this.isAuthenticated = true;
      return true;
    })
    .catch((reason: any) => {
      return Promise.reject(false);
    });
    return promise;
  }


}
