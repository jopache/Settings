import { TreeNodeSelector } from './treeNodeSelector';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';

import { TreeNode } from '../treenode';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { environment } from '../../environments/environment';

@Injectable()
export class EnvironmentService implements TreeNodeSelector {
  private getAllEnvironments = environment.backendUrl + '/api/environments/';

  constructor(private http: HttpClient) { }

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
        return response as TreeNode;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject('fail');
      });
  }
}
