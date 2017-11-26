import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

export interface Token {
  token: string;
}
@Injectable()
export class AuthorizationService {
  private authUrl = environment.backendUrl + '/api/authorization/jwt';
  private localStorageTokenKey = 'jwt_token';
  private token: string = null;

  constructor(private http: HttpClient) {
  }

  isAuthenticated(): boolean {
    if (this.token) {
      return true;
    } else {
      const token = this.getToken();
      if (token) {
        this.token = token;
        return true;
      }
    }
    return false;
  }

  getToken(): string {
    return this.token ? this.token : localStorage.getItem(this.localStorageTokenKey);
  }

  authenticate(username: string, password: string): Promise<boolean> {
    return this.http.post<Token>(this.authUrl, {
      userName: username,
      password: password
    })
    .toPromise()
    .then(data => {
      this.token = data.token;
      localStorage.setItem(this.localStorageTokenKey, this.token);
      return true;
    })
    .catch((reason: any) => {
      return Promise.reject(false);
    });
  }
}
