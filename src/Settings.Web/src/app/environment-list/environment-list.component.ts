import { Component, OnInit, Input } from '@angular/core';
import { TreeNode } from '../treenode';
import { environment } from '../../environments/environment';
import { EnvironmentService } from '../services/environment.service';

@Component({
  selector: 'app-environment-list',
  templateUrl: './environment-list.component.html',
  styleUrls: ['./environment-list.component.scss']
})
export class EnvironmentListComponent implements OnInit {

  constructor(public environmentService: EnvironmentService) { }

  @Input() environments: TreeNode[];
  @Input() isLoading: Boolean;

  ngOnInit() {
    console.log('env list init');
  }

}
