import { Component, OnInit, Input } from '@angular/core';
import {SettingsService} from '../services/settings.service';
import {TreeNode} from '../treenode';

@Component({
  selector: 'app-crud-setting',
  templateUrl: './crud-setting.component.html',
  styleUrls: ['./crud-setting.component.scss']
})
export class CrudSettingComponent implements OnInit {

  @Input() selectedApplication: TreeNode = null;
  @Input() selectedEnvironment: TreeNode = null;
  message: string = null;


  // todo: once again, push this out to its own class
  editSettingModel: { setting: any, appName: string, envName: string}  =  null;

  // todo: make the call to persist settings pass in the setting name/value
  addEdit(): void {
    if (!this.editSettingModel.setting.name) {
      // TODO: Add real validation
      this.message = 'Setting must have a valid name';
      return;
    } else {
      this.message = null;
    }
    this.settingsService.persistSetting()
      .then(done => {

        // trying this out, not ideal, but want to see if it works.  Otherwise emit an event to parent?
        // wondering about that vs doing some kind of flux like pattern for the whole application
        const app = this.selectedApplication;
        const env = this.selectedEnvironment;

      });
    this.settingsService.setEditModel({ name: '', value: ''});
  }
  constructor(private settingsService: SettingsService) { }

  ngOnInit() {
    this.editSettingModel = this.settingsService.getEditModel();
  }

}
