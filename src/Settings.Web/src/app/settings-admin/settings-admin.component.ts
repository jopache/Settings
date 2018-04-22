import { Component, OnInit } from '@angular/core';
import { TreeNode, PermissionsAggregateModel } from '../treenode';
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
  selectedAppEnvPermissions: PermissionsAggregateModel = null;

  appsLoading = true;
  envsLoading = true;

  applications: TreeNode[] = null;
  environments: TreeNode[] = null;

  ngOnInit(): void {
    this.activeAppNode$.subscribe(app => {
      if (app !== null) {
        this.selectedApplication = app;
        this.envsLoading = true;
        this.environmentService.setActiveNode(null);
        this.environmentService.getEnvironmentsForApplication(app.name)
          .then(envs => {
            this.environments = envs;
            this.envsLoading = false;
          });
      }
    });

    this.activeEnvNode$.subscribe(env => {
      this.selectedEnvironment = env;
      if (env !== null) {
        this.selectedAppEnvPermissions = env.aggregatePermissions;
      }
    });

    this.applicationService
      .getApplications()
      .then(applications => {
        this.appsLoading = false;
        this.applications = applications;
        this.applicationService.setActiveNode(applications[0]);
      });
  }
}
