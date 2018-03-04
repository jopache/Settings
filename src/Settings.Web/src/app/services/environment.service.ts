import { TreeNodeService } from './treeNode.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';

import { TreeNode } from '../treenode';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { environment } from '../../environments/environment';

@Injectable()
export class EnvironmentService implements TreeNodeService {
  private baseUrl = environment.backendUrl + '/api/environments/';

  constructor(private http: HttpClient) { }

  private _activeNode: BehaviorSubject<TreeNode> = new BehaviorSubject<TreeNode>(null);
  activeNode = this._activeNode.asObservable();

  setActiveNode(node: TreeNode): void {
    this._activeNode.next(node);
  }

  getRootEnvironment(): Promise<TreeNode> {

    return this.http
      .get(this.baseUrl)
      .toPromise()
      .then(response => {
        return response as TreeNode;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject('fail');
      });
  }

  createChildNode(parentId: number, nodeName: string): Promise<any> {
    return this.createEnvironment(parentId, nodeName);
  }

  createEnvironment(parentId: number, name: string): Promise<any> {
    const url = this.baseUrl + `add/parent-${parentId}/new-${name}/`;
    return this.http.post(url, {})
    .toPromise()
    .then(response => {
      return response as TreeNode;
    })
    .catch(blah => {
      console.error('failure creating environment.');
    });
  }
}
