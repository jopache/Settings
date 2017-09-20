import { Component, OnInit } from '@angular/core';
import { TreeNode } from './treenode';
import { ApplicationService } from './services/application.service';
import { EnvironmentService } from './services/environment.service';
import { SettingsService } from './services/settings.service';
import { Observable } from 'rxjs/Observable';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(public applicationService: ApplicationService, public environmentService: EnvironmentService) {

      this.activeAppNode$.subscribe(app => {
        if (app !== null) {
          this.selectedApplication = app;
        }
      });

      this.activeEnvNode$.subscribe(env => {
        if (env !== null) {
          this.selectedEnvironment = env;
        }
      });

    }

  activeAppNode$ = this.applicationService.activeNode;
  activeEnvNode$ = this.environmentService.activeNode;

  selectedApplication: TreeNode = null;
  selectedEnvironment: TreeNode = null;

  // need to rethink this. Eventually there may not be a root, there may be several "roots"
  rootApplication: TreeNode = null;
  rootEnvironment: TreeNode = null;

  ngOnInit(): void {
    this.applicationService
      .getRootApplication()
      .then(application => {
        console.log('setting root application');
        this.rootApplication = application;
        this.selectedApplication = application;
        this.applicationService.setActiveNode(application);
      });

    this.environmentService
      .getRootEnvironment()
      .then(environment => {
        this.rootEnvironment = environment;
        this.selectedEnvironment = environment;
        this.environmentService.setActiveNode(environment);
      });
  }
}
