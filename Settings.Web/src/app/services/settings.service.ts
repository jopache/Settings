import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';


@Injectable()
export class SettingsService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private getSettingsBaseUrl = 'http://localhost:59579/api/settings/';

  constructor(private http: Http) { }

  getSettings(appName: string, envName: string): Promise<any> {
    return this.http
      .get(this.getSettingsBaseUrl + appName + '/' + envName + '/')
      .toPromise()
      .then(response => {
        return response.json();
      })
      .catch((error: any ): Promise<any> => {
        return Promise.reject("fail");
      });
  }
}
