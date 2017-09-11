import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { TreeNode } from '../treenode';

@Injectable()
export class ApplicationService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private getAllApplicationsUrl = 'http://40.71.223.176:8001/api/applications/';

  constructor(private http: Http) { }

  getRootApplication(): Promise<TreeNode> {

    return this.http
      .get(this.getAllApplicationsUrl)
      .toPromise()
      .then(response => {
        var node = response.json() as TreeNode;
        //node.active = false;
        return node;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject("fail");
      });
  }
}
