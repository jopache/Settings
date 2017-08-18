import { Component, OnInit } from '@angular/core';
import { TreeNode } from './treenode'
import { ApplicationService } from './services/application.service';
import { EnvironmentService } from './services/environment.service';

//remove hard coded data after fetching from service

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private applicationService: ApplicationService,
  private environmentService: EnvironmentService) { }

  ngOnInit(): void {
    this.applicationService
      .getRootApplication()
      .then(application => {
        this.rootApplication = application;
      });

    this.environmentService
      .getRootEnvironment()
      .then(environment => {
        this.rootEnvironment = environment;
      });
  }

  //need to fix this with a simple ngif in template
  rootApplication: TreeNode = { name: 'Global', id: 0, children: [] };
  rootEnvironment: TreeNode = { name: 'All', id: 0, children: []};
  title = 'app';
}
