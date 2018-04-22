import { Component, OnInit, Input } from '@angular/core';
import { TreeNode } from '../treenode';
import { ApplicationService } from '../services/application.service';

@Component({
  selector: 'app-application-list',
  templateUrl: './application-list.component.html',
  styleUrls: ['./application-list.component.scss']
})
export class ApplicationListComponent implements OnInit {

  constructor(public applicationService: ApplicationService) { }
  @Input() applications: TreeNode[] = null;
  @Input() isLoading = true;

  ngOnInit() {
    console.log('app list init');
  }

}
