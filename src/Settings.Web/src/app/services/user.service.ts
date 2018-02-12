import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';
import { User } from '../models/user';

@Injectable()
export class UserService {
  private baseUrl = environment.backendUrl + '/api/users/';

  constructor(private http: HttpClient) { }

  createUser(username: string, isAdmin: boolean, password: string): Promise<any> {
    const url = this.baseUrl + 'add/';
    return this.http.post(url, {
      username: username,
      isAdmin: isAdmin,
      password: password
    })
    .toPromise()
    .then( () => {
      console.log('success adding user');
      return Promise.resolve();
    })
    .catch(resp => {
      return Promise.reject(resp.error);
    });
  }

  loadUsers(): Promise<User[]> {
    const url = this.baseUrl;
    return this.http.get(url)
      .toPromise()
      .then(response => {
        return Promise.resolve(<User[]>response);
      })
      .catch(error => {
        return Promise.reject(error.error);
      });
  }
}
