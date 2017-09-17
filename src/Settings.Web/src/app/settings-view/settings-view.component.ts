import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { TreeNode } from '../treenode';
import { SettingsService } from '../services/settings.service';

@Component({
  selector: 'app-settings-view',
  templateUrl: './settings-view.component.html',
  styleUrls: ['./settings-view.component.scss']
})
export class SettingsViewComponent implements OnChanges {
  @Input() selectedApplication: TreeNode;
  @Input() selectedEnvironment: TreeNode;
  settings: any;

  ngOnChanges(changes: SimpleChanges) {
    this.updateSettings();
  }

  constructor(private settingsService: SettingsService) {
    this.settingsService.settingsUpdated$.subscribe(x => {
      this.updateSettings();
    });
  }

  setEditSetting(editModel: { name: string, value: string}): void {
    this.settingsService.setEditModel({name: editModel.name, value: editModel.value});
  }

  updateSettings(): void {
    if (this.selectedApplication && this.selectedEnvironment) {
      const appName = this.selectedApplication.name;
      const envName = this.selectedEnvironment.name;

      this.settingsService.getSettings(appName, envName)
        .then(settings => {
          for (const setting of settings) {
            setting.sourceData = null;

            if (this.selectedApplication.id !== setting.applicationId ||
              this.selectedEnvironment.id !== setting.environmentId) {
              setting.sourceData = setting.applicationName + ' - ' + setting.environmentName;
            }
          }
          this.settings = settings;
        });
    }
  }
}


