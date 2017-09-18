import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { TreeNode } from '../treenode';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { TreeNodeSelector } from './treeNodeSelector';
import { environment } from '../../environments/environment';

@Injectable()
export class ApplicationService implements TreeNodeSelector {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private getAllApplicationsUrl = environment.backendUrl + '/api/applications/';

  constructor(private http: Http) { }

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
        const node = response.json() as TreeNode;
        return node;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject('fail');
      });
  }
}
