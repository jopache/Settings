import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthenticationService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private authUrl = environment.backendUrl + '/api/authentication/jwt';
  private localStorageTokenKey = 'jwt_token';
  token: string = null;

  constructor(private http: Http) { }

  isAuthenticated(): boolean {
    if (this.token === null) {
      const lsToken = localStorage.getItem(this.localStorageTokenKey);
      if (lsToken !== null) {
        this.token = lsToken;
      }
    }

    if (this.token) {
      return true;
    }
    return false;
  }
  authenticate(username: string, password: string): Promise<boolean> {

    const promise = this.http.post(this.authUrl, {
      userName: username,
      password: password
    })
    .toPromise()
    .then(response => {
      this.token = response.json().token;
      localStorage.setItem(this.localStorageTokenKey, this.token);
      return true;
    })
    .catch((reason: any) => {
      return Promise.reject(false);
    });
    return promise;
  }


}
