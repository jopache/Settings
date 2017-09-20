import { TreeNodeSelector } from './treeNodeSelector';
import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { TreeNode } from '../treenode';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { environment } from '../../environments/environment';

@Injectable()
export class EnvironmentService implements TreeNodeSelector {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private getAllEnvironments = environment.backendUrl + '/api/environments/';

  constructor(private http: Http) { }

  private _activeNode: BehaviorSubject<TreeNode> = new BehaviorSubject<TreeNode>(null);
  activeNode = this._activeNode.asObservable();

  setActiveNode(node: TreeNode): void {
    this._activeNode.next(node);
  }

  getRootEnvironment(): Promise<TreeNode> {

    return this.http
      .get(this.getAllEnvironments)
      .toPromise()
      .then(response => {
        return response.json() as TreeNode;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject('fail');
      });
  }
}
