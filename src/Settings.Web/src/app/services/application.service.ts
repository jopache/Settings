import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { TreeNode } from '../treenode';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { TreeNodeService } from './treeNode.service';
import { environment } from '../../environments/environment';

@Injectable()
export class ApplicationService implements TreeNodeService {
  private baseUrl = environment.backendUrl + '/api/applications/';

  constructor(private http: HttpClient) { }

  // TODO: Probably can change this to just Subject as my initial value is null anyways
  private _activeNode: BehaviorSubject<TreeNode> = new BehaviorSubject<TreeNode>(null);
  activeNode = this._activeNode.asObservable();

  setActiveNode(node: TreeNode): void {
    this._activeNode.next(node);
  }
  getRootApplication(): Promise<TreeNode> {
    return this.http
      .get(this.baseUrl)
      .toPromise()
      .then(response => {
        const node = response as TreeNode;
        return node;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject('fail');
      });
  }
  createChildNode(parentId: number, nodeName: string): Promise<any> {
    return this.createApplication(parentId, nodeName);
  }

  createApplication(parentId: number, applicationName: string): Promise<any> {
    const url = this.baseUrl + `add/parent-${parentId}/new-${applicationName}/`;
    return this.http.post(url, {})
    .toPromise()
    .then(response => {
      console.log('success');
      return response as TreeNode;
    })
    .catch(blah => {
      console.log('fail');
    });
  }
}
