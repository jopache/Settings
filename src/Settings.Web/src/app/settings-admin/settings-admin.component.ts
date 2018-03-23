import { Component, OnInit } from '@angular/core';
import { TreeNode } from '../treenode';
import { ApplicationService } from '../services/application.service';
import { EnvironmentService } from '../services/environment.service';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs/Observable';
@Component({
  selector: 'app-settings-admin',
  templateUrl: './settings-admin.component.html',
  styleUrls: ['./settings-admin.component.scss']
})
export class SettingsAdminComponent implements OnInit {
  constructor(public applicationService: ApplicationService, public environmentService: EnvironmentService) {
  }

  activeAppNode$ = this.applicationService.activeNode;
  activeEnvNode$ = this.environmentService.activeNode;

  selectedApplication: TreeNode = null;
  selectedEnvironment: TreeNode = null;

  appsLoaded = false;
  envsLoaded = false;

  applications: TreeNode[] = null;
  environments: TreeNode[] = null;

  ngOnInit(): void {
    this.activeAppNode$.subscribe(app => {
      if (app !== null) {
        this.selectedApplication = app;
        if (app !== null) {
          this.environmentService.getEnvironmentsForApplication(app.name)
            .then(envs => {
              this.environments = envs;
              this.envsLoaded = true;
            });
        }
      }
    });

    this.activeEnvNode$.subscribe(env => {
      if (env !== null) {
        this.selectedEnvironment = env;
      }
    });

    this.applicationService
      .getApplications()
      .then(applications => {
        if (!this.appsLoaded) {
          this.appsLoaded = true;
        }
        this.applications = applications;
        this.applicationService.setActiveNode(applications[0]);
        this.envsLoaded = false;
      });

    // this.environmentService
    //   .getRootEnvironment()
    //   .then(environment => {
    //     this.rootEnvironment = environment;
    //     this.environmentService.setActiveNode(environment);
    //   });
  }
}
