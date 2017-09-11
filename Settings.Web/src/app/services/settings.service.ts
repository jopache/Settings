import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';


@Injectable()
export class SettingsService {
    private getSettingsBaseUrl = 'http://40.71.223.176:8001/api/settings/';
  private persistSettingBaseUrl
  = 'http://40.71.223.176:8001/api/settings/create-update/';

  //TODO: Need to create some types around this instead of this hackiness
  private currentEditModel:
    { setting: any, envName: string, appName: string } =
    { setting: { name : "", value: "" }, envName: null, appName: null };

  constructor(private http: Http) { }

  getSettings(appName: string, envName: string): Promise<any> {
    return this.http
      .get(this.getSettingsBaseUrl + appName + '/' + envName + '/')
      .toPromise()
      .then(response => {
        this.currentEditModel.appName = appName;
        this.currentEditModel.envName = envName;

        return response.json();
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject("fail");
      });
  }

  getEditModel() :  { setting: any, envName: string, appName: string }{
    return this.currentEditModel;
  }

  setEditModel(editModel: { name: string, value: string}) {
    this.currentEditModel.setting = editModel;
  }

  persistSetting(): Promise<any>{
    var app = this.currentEditModel.appName;
    var env = this.currentEditModel.envName;
    var url = this.persistSettingBaseUrl + app + '/'  + env + '/';
    //var url = `{this.persistSettingBaseUrl}{app}/{env}`;
    return this.http.post(url, {
      settingsToUpdate: [
        this.currentEditModel.setting
      ]
    }).toPromise();
  }

}
