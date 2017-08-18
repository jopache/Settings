import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { TreeNode } from '../treeNode';


const tree: TreeNode = {
  name: "global", id: 1, children: [
    {
      name: "nested", id: 2, children: [
        {
          name: "nested-console", id: 3, children: [
            { name: "nested-console-again", id: 3, children: [] }
          ]
        }
      ]
    },
    { name: "nested-web app", id: 3, children: [] }
  ]
};


@Injectable()
export class ApplicationService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private getAllApplicationsUrl = 'http://localhost:59579/api/applications/';

  constructor(private http: Http) { }

  getRootApplication(): Promise<TreeNode> {

    return this.http
      .get(this.getAllApplicationsUrl)
      .toPromise()
      .then(response => {
        return response.json() as TreeNode;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject("fail");
      });
  }
}
