import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { environment } from '../../environments/environment';

@Injectable()
export class SettingsService {
  private getSettingsBaseUrl = environment.backendUrl + '/api/settings/';
  private persistSettingBaseUrl = environment.backendUrl + '/api/settings/create-update/';
  private _settingsUpdated = new Subject();
  settingsUpdated$ = this._settingsUpdated.asObservable();

  // TODO: Need to create some types around this instead of this hackiness
  private currentEditModel:
    { setting: any, envName: string, appName: string } =
    { setting: { name : '', value: '' }, envName: null, appName: null };

  constructor(private http: HttpClient) { }

  getSettings(appName: string, envName: string): Promise<any> {
    return this.http
      .get(this.getSettingsBaseUrl + appName + '/' + envName + '/')
      .toPromise()
      .then(response => {
        this.currentEditModel.appName = appName;
        this.currentEditModel.envName = envName;

        return response;
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject('fail');
      });
  }

  getEditModel():  { setting: any, envName: string, appName: string } {
    return this.currentEditModel;
  }

  setEditModel(editModel: { name: string, value: string}) {
    this.currentEditModel.setting = editModel;
  }

  persistSetting(): Promise<any> {
    const app = this.currentEditModel.appName;
    const env = this.currentEditModel.envName;
    const url = this.persistSettingBaseUrl + app + '/'  + env + '/';
    // var url = `{this.persistSettingBaseUrl}{app}/{env}`;
    return this.http.post(url, {
      settingsToUpdate: [
        this.currentEditModel.setting
      ]
    })
    .toPromise()
    .then(response => {
      this._settingsUpdated.next();
    });
  }

}
