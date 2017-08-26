import { Component, OnInit } from '@angular/core';
import { TreeNode } from './treenode'
import { ApplicationService } from './services/application.service';
import { EnvironmentService } from './services/environment.service';
import {SettingsService} from "./services/settings.service";

//remove hard coded data after fetching from service

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private applicationService: ApplicationService,
    private environmentService: EnvironmentService, private settingsService: SettingsService) { }


  ngOnInit(): void {
    this.applicationService
      .getRootApplication()
      .then(application => {
        this.rootApplication = application;
        this.selectedApplicationModel.node = application;
      });

    this.environmentService
      .getRootEnvironment()
      .then(environment => {
        this.rootEnvironment = environment;
        this.selectedEnvironmentModel.node = environment;
      });
  }

  //need to fix this with a simple ngif in template
  rootApplication: TreeNode = null;
  rootEnvironment: TreeNode = null;

  selectedApplicationModel: { node: TreeNode } = { node: null };
  selectedEnvironmentModel: { node: TreeNode } = { node: null };
}
