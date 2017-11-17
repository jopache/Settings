import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { TreeNode } from '../treenode';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { TreeNodeSelector } from './treeNode.service';
import { environment } from '../../environments/environment';

@Injectable()
export class ApplicationService implements TreeNodeSelector {
  private getAllApplicationsUrl = environment.backendUrl + '/api/applications/';

  constructor(private http: HttpClient) { }

  // TODO: Probably can change this to just Subject as my initial value is null anyways
  private _activeNode: BehaviorSubject<TreeNode> = new BehaviorSubject<TreeNode>(null);
  activeNode = this._activeNode.asObservable();

  setActiveNode(node: TreeNode): void {
    this._activeNode.next(node);
  }
  getRootApplication(): Promise<TreeNode> {
    return this.http
      .get(this.getAllApplicationsUrl)
      .toPromise()
      .then(response => {
        const node = response as TreeNode;
        return node;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject('fail');
      });
  }
}
