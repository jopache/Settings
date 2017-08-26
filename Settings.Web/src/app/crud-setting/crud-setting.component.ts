import { Component, OnInit } from '@angular/core';
import {SettingsService} from "../services/settings.service";

@Component({
  selector: 'app-crud-setting',
  templateUrl: './crud-setting.component.html',
  styleUrls: ['./crud-setting.component.scss']
})
export class CrudSettingComponent implements OnInit {

  //todo: once again, push this out to its own class
  editSettingModel: { setting: any, appName: string, envName: string}  =  null;
  addEdit(): void {
    console.log(this.editSettingModel.setting.name, this.editSettingModel.setting.value, this.editSettingModel.appName, this.editSettingModel.envName);
  }
  constructor(private settingsService: SettingsService) { }

  ngOnInit() {
    this.editSettingModel = this.settingsService.getEditModel();
  }

}
