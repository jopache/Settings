import { Component, OnInit, Input } from '@angular/core';
import {SettingsService} from "../services/settings.service";
import {TreeNode} from "../treenode";

@Component({
  selector: 'app-crud-setting',
  templateUrl: './crud-setting.component.html',
  styleUrls: ['./crud-setting.component.scss']
})
export class CrudSettingComponent implements OnInit {

  @Input() selectedApplicationModel: { node: TreeNode } = { node: null };
  @Input() selectedEnvironmentModel: { node: TreeNode } = { node: null };
  message: string = null;


  //todo: once again, push this out to its own class
  editSettingModel: { setting: any, appName: string, envName: string}  =  null;
  addEdit(): void {
    if (!this.editSettingModel.setting.name) {
      //TODO: Add real validation
      this.message = "Setting must have a valid name";
      return;
    } else {
      this.message = null;
    }
    console.log(this.editSettingModel.setting.name, this.editSettingModel.setting.value, this.editSettingModel.appName, this.editSettingModel.envName);
    this.settingsService.persistSetting()
      .then(done => {

        //trying this out, not ideal, but want to see if it works.  Otherwise emit an event to parent?
        //wondering about that vs doing some kind of flux like pattern for the whole application
        var app = this.selectedApplicationModel.node;
        var env = this.selectedEnvironmentModel.node;
        this.selectedApplicationModel.node = { name: app.name, id: app.id, children: app.children }
        this.selectedEnvironmentModel.node = { name: env.name, id: env.id, children: env.children }
      });
    this.settingsService.setEditModel({ name: "", value: ""});
  }
  constructor(private settingsService: SettingsService) { }

  ngOnInit() {
    this.editSettingModel = this.settingsService.getEditModel();
  }

}
