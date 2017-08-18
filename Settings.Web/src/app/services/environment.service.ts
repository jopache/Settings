import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { TreeNode } from '../treeNode';

@Injectable()
export class EnvironmentService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private getAllEnvironments = 'http://localhost:59579/api/environments/';

  constructor(private http: Http) { }

  getRootEnvironment(): Promise<TreeNode> {

    return this.http
      .get(this.getAllEnvironments)
      .toPromise()
      .then(response => {
        return response.json() as TreeNode;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject("fail");
      });
  }
}
