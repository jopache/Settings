import { TreeNodeSelector } from './treeNodeSelector';
import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthenticationService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private getAllEnvironments = environment.backendUrl + '/api/environments/';

  isAuthenticated = false;

  constructor(private http: Http) { }

  authenticate(username: string, password: string): boolean {
      if (username === password) {
        this.isAuthenticated = true;
      } else {
        this.isAuthenticated = false;
      }
      return this.isAuthenticated;
  }


}
