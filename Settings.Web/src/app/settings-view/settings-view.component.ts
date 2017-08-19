import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { TreeNode } from '../treeNode';
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


  constructor(private settingsService: SettingsService) { }

  updateSettings(): void {
    console.log(this.selectedApplication);
    console.log(this.selectedEnvironment);
    if (this.selectedApplication && this.selectedEnvironment) {
      let appName = this.selectedApplication.name;
      let envName = this.selectedEnvironment.name;

      this.settingsService.getSettings(appName, envName)
        .then(settings => {
          //Should probably return as an array from backend? 
          //should probably type this? this is getting hacky
          var keys = Object.keys(settings);
          var arr = [];
          for (let theKey of keys) {
            arr.push({ key : theKey, value : settings[theKey]});
          }
          this.settings = arr;
        });
    }

  }
    
}


