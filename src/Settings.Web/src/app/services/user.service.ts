import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

@Injectable()
export class UserService {
  private baseUrl = environment.backendUrl + '/api/users/';

  constructor(private http: HttpClient) { }

  createUser(username: string, isAdmin: boolean): Promise<any> {
    const url = this.baseUrl + 'add/';
    return this.http.post(url, {})
    .toPromise()
    .then(response => {
      console.log('success adding user');
      return true;
    })
    .catch(blah => {
      console.log('fail');
    });
  }
}
